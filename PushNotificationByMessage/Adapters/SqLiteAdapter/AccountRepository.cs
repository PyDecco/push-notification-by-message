using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PushNotificationByMessage.Models.Dtos;
using PushNotificationByMessage.Models.Entites;
using PushNotificationByMessage.Ports.Out;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PushNotificationByMessage.Adapters.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<User> userManager,
            SignInManager<User> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<IdentityResult> SignUpAsync(UserRegisterDto signUpModel)
        {
            var user = new User()
            {
                Name = signUpModel.Name,
                PhoneNumber = signUpModel.Telephone,
                Address = signUpModel.Address,
                Email = signUpModel.Email,
                UserName = signUpModel.Email
            };

            return await _userManager.CreateAsync(user, signUpModel.Password);
        }

        public async Task<string> LoginAsync(LoginDto signInModel)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(signInModel.Login, signInModel.Password, false, false);

                if (!result.Succeeded)
                {
                    return null;
                }

                var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, signInModel.Login),
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
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
