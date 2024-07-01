using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Pagination;

namespace Ecommerce.Application.Interfaces
{
    public interface ICarrinhoService
    {
        Task<CarrinhoDto> Incluir(CarrinhoDto carrinhoDto);
        Task<CarrinhoDto> Alterar(CarrinhoDto carrinhoDto);
        Task<CarrinhoDto> Excluir(int id);
        Task<CarrinhoDto> SelecionarAsync(int id);
        Task<IEnumerable<CarrinhoDto>> SelecionarCarrinhoAbertoAsync(CarrinhoAbertoDto obj);
        Task<PagedList<CarrinhoDto>> SelecionarTodosAsync(int pageNumber, int pageSize);
    }
}
