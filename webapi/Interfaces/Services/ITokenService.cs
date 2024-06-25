using Ecommerce.API.Dto;

namespace Ecommerce.Interfaces.Services
{
    public interface ITokenService
    {
        string CreateToken(LoginDto userLogin);
    }
}
