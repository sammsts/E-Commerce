using AutoMapper;
using Ecommerce.API.Dto;
using Ecommerce.API.Models;

namespace Ecommerce.API.Mappings
{
    public class EntitiesToDtoMappingProfile : Profile
    {
        public EntitiesToDtoMappingProfile()
        {
            CreateMap<Usuarios, UsuarioDto>().ReverseMap();
        }
    }
}
