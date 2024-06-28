using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Pagination;

namespace Ecommerce.Application.Interfaces
{
    public interface IEnderecoService
    {
        Task<EnderecoDto> Incluir(EnderecoDto enderecoDto);
        Task<EnderecoDto> Alterar(EnderecoDto enderecoDto);
        Task<EnderecoDto> Excluir(int id);
        Task<EnderecoDto> SelecionarAsync(int Usu_id);
        Task<PagedList<EnderecoDto>> SelecionarTodosAsync(int pageNumber, int pageSize);
    }
}
