using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Pagination;

namespace Ecommerce.Application.Interfaces
{
    public interface IPedidoService
    {
        Task<PedidoDto> Incluir(PedidoDto pedidoDto);
        Task<PedidoDto> Alterar(PedidoDto pedidoDto);
        Task<PedidoDto> Excluir(int id);
        Task<PedidoDto> SelecionarAsync(int id);
        Task<PagedList<PedidoDto>> SelecionarTodosAsync(int pageNumber, int pageSize);
    }
}
