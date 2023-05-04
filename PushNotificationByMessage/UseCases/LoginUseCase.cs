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
        private readonly IJWTCore _jwtCore;
        private readonly IBCrypt _bcrypt;

        public LoginUseCase(IGenericRepository<User> userRepo, IJWTCore jwtCore, IBCrypt bcrypt)
        {
            _usersRepo = userRepo;
            _jwtCore = jwtCore;
            _bcrypt = bcrypt;
        }

        public async Task<LoginToReturnDto> Login(LoginDto loginRequest)
        {
            var dbUser = await VerificaSeEmailEncontrado(loginRequest.Login);
            
            await VerificarLoginESenha(loginRequest, dbUser);
            var jwt = await _jwtCore.GeradorDeJwt(loginRequest.Login);

            var gerarResult = await GeradorDeResult(dbUser, jwt);

            return gerarResult;

        }

        private async Task<User> VerificaSeEmailEncontrado(string email)
        {
            var dbUser = await _usersRepo.GetByEmaildAsync(email);

            if (dbUser == null)   
            {
                throw new UnauthorizedAccessException("Usuario Nao encontrado");
            }   

            return dbUser;
        }

        private async Task<bool> VerificarLoginESenha(LoginDto loginRequest, User dbUser)
        {
            var senhaCompativel = _bcrypt.Verify(loginRequest.Password, dbUser.Password);
            
            if (!senhaCompativel)
            {
                throw new UnauthorizedAccessException("Login e password não coicidem");
            }

            return senhaCompativel;

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
