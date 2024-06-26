using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Domain.Entities;

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
