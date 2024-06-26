using Ecommerce.API.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ecommerce.Interfaces.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _chave;
        public TokenService(IConfiguration config)
        {
            _chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["chaveSecreta"]));
        }

        public string CreateToken(LoginDto userLogin)
        {
            var claims = new List<Claim>
            {
                new Claim
                (JwtRegisteredClaimNames.NameId,
                userLogin.Nome)
            };

            var credenciais = new SigningCredentials(_chave, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor 
            { 
                Subject = new ClaimsIdentity(claims), 
                Expires = DateTime.Now.AddDays(7) , 
                SigningCredentials = credenciais 
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
