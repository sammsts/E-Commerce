using Ecommerce.Domain.Account;
using Ecommerce.Domain.Entities;
using Ecommerce.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly EcommerceContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticateService(EcommerceContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> AuthenticateAsync(string email, string senha)
        {
            var usuario = await _context.Usuarios.Where(x => x.Usu_email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if (usuario == null) 
            {
                return false;
            }

            using var hmac = new HMACSHA512(usuario.Usu_senhaSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));

            for (int x = 0; x< computedHash.Length; x++) 
            {
                if (computedHash[x] != usuario.Usu_senhaHash[x])
                {
                    return false;
                }
            }

            return true;
        }

        public string GenerateToken(int id, string email)
        {
            var claims = new[]
            {
                new Claim("id", id.ToString()),
                new Claim("email", email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:secretKey"]));
            var credentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(30);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["jwt:issuer"],
                audience: _configuration["jwt:audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> UserExists(string email)
        {
            var usuario = await _context.Usuarios.Where(x => x.Usu_email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            if (usuario == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Usuarios> GetUserByEmail(string email)
        {
            return await _context.Usuarios.Where(x => x.Usu_email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }
    }
}
