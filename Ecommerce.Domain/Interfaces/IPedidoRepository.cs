using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Pagination;

namespace Ecommerce.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedidos> Incluir(Pedidos pedido);
        Task<Pedidos> Alterar(Pedidos pedido);
        Task<Pedidos> Excluir(int id);
        Task<Pedidos> SelecionarAsync(int id);
        Task<PagedList<Pedidos>> SelecionarTodosAsync(int pageNumber, int pageSize);
    }
}
