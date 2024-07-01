using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Pagination;

namespace Ecommerce.Domain.Interfaces
{
    public interface ICarrinhoRepository
    {
        Task<Carrinho> Incluir(Carrinho carrinho);
        Task<Carrinho> Alterar(Carrinho carrinho);
        Task<Carrinho> AlterarSituacaoCarrinho(int id);
        Task<Carrinho> Excluir(int id);
        Task<Carrinho> SelecionarAsync(int id);
        Task<IEnumerable<Carrinho>> SelecionarCarrinhoAbertoAsync(int Usu_Id, int End_Id);
        Task<PagedList<Carrinho>> SelecionarTodosAsync(int pageNumber, int pageSize);
    }
}
