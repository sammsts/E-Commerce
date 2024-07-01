using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Entities;

namespace Ecommerce.API.Mappings
{
    public class EntitiesToDtoMappingProfile : Profile
    {
        public EntitiesToDtoMappingProfile()
        {
            CreateMap<Usuarios, UsuarioDto>().ReverseMap();
            CreateMap<Enderecos, EnderecoDto>().ReverseMap();
            CreateMap<Produtos, ProdutoDto>().ReverseMap();
            CreateMap<Carrinho, CarrinhoDto>().ReverseMap();
            CreateMap<Pedidos, PedidoDto>().ReverseMap();
        }
    }
}
