using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Account
{
    public interface IAuthenticate
    {
        Task<bool> AuthenticateAsync(string email, string senha);
        Task<bool> UserExists(string email);
        public string GenerateToken(int id, string email);
        public Task<Usuarios> GetUserByEmail(string email);
    }
}
