using Ecommerce.API.Dto;
using Ecommerce.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    public class AuthController : Controller
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        public async Task<ActionResult<UserAuthDto>> Login(LoginDto loginDto)
        {
            if (loginDto == null) 
            { 
                throw new ArgumentNullException(nameof(loginDto)); 
            }

            return new UserAuthDto
            {
                Username = loginDto.Nome,
                token = _tokenService.CreateToken(loginDto)
            };
        }
    }
}
