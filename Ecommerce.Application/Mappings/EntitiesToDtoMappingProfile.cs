﻿using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;

namespace Ecommerce.API.Mappings
{
    public class EntitiesToDtoMappingProfile : Profile
    {
        public EntitiesToDtoMappingProfile()
        {
            CreateMap<Usuarios, UsuarioDto>().ReverseMap();
            CreateMap<Usuarios, AtualizaUsuarioDto>().ReverseMap();
            CreateMap<Enderecos, EnderecoDto>().ReverseMap();
        }
    }
}
