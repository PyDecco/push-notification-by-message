using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PushNotificationByMessage.Models.Dtos;
using PushNotificationByMessage.Models.Entites;
using PushNotificationByMessage.Ports.In;
using PushNotificationByMessage.Ports.Out;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PushNotificationByMessage.UseCases
{
    public class LoginUseCase : ILoginUseCase
    {
        private readonly IGenericRepository<User> _usersRepo;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;

        public LoginUseCase(IGenericRepository<User> userRepo, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _usersRepo = userRepo;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public async Task<LoginToReturnDto> Login(LoginDto loginRequest)
        {
            var user = await VerificaSeEmailEncontrado(loginRequest.Login);
            
            await VerificarLoginESenha(loginRequest);
            var jwt = await GeradorDeJwt(loginRequest.Login);

            var gerarResult = await GeradorDeResult(user, jwt);

            return gerarResult;

        }

        private async Task<User> VerificaSeEmailEncontrado(string login)
        {
            var user = await _usersRepo.GetByEmaildAsync(login);

            if (user == null)   
            {
                throw new UnauthorizedAccessException("Usuario Nao encontrado");
            }   

            return user;
        }

        private async Task<SignInResult> VerificarLoginESenha(LoginDto loginRequest)
        {
            var result = await _signInManager.PasswordSignInAsync(loginRequest.Login, loginRequest.Password, false, false);

            if (!result.Succeeded)
            {
                throw new UnauthorizedAccessException("Login e password não coicidem");
            }

            return result;

        }

        private async Task<string> GeradorDeJwt(string login)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var authSigninKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<LoginToReturnDto> GeradorDeResult(User user, string jwt)
        {
            return new LoginToReturnDto()
            {
                Token = jwt,
                User = new UserLogin() { id = user.Id, name = user.Name, email = user.Email, }
            };
        }
    }
}
