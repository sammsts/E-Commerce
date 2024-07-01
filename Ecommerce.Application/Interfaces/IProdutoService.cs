using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Pagination;

namespace Ecommerce.Application.Interfaces
{
    public interface IProdutoService
    {
        Task<ProdutoDto> Incluir(ProdutoDto produtoDto);
        Task<ProdutoDto> Alterar(ProdutoDto produtoDto);
        Task<ProdutoDto> Excluir(int serialId);
        Task<ProdutoDto> SelecionarAsync(int serialId);
        Task<PagedList<ProdutoDto>> SelecionarTodosAsync(int pageNumber, int pageSize);
    }
}
