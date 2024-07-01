using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Pagination;

namespace Ecommerce.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<Produtos> Incluir(Produtos produto);
        Task<Produtos> Alterar(Produtos produto);
        Task<Produtos> Excluir(int id);
        Task<Produtos> SelecionarAsync(int id);
        Task<PagedList<Produtos>> SelecionarTodosAsync(int pageNumber, int pageSize);
    }
}
