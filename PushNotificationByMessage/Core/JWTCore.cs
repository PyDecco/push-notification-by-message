using Microsoft.IdentityModel.Tokens;
using PushNotificationByMessage.Ports.In;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PushNotificationByMessage.Core
{
    public class JWTCore : IJWTCore
    {
        private readonly IConfiguration _configuration;


        public JWTCore(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GeradorDeJwt(string login)
        {

            var authClaims = GeradorDeAuthClaims(login);
            var authSigninKey = DefinidorDeConfiguracao();
            return GeradorDeToken(authClaims, authSigninKey);
        }

        private string GeradorDeToken(List<Claim> authClaims, SymmetricSecurityKey authSigninKey)
        {
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private SymmetricSecurityKey DefinidorDeConfiguracao()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));
        }

        private List<Claim> GeradorDeAuthClaims(string login)
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Name, login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
        }
    }
}
