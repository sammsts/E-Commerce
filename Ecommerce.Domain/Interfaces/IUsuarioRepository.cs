using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Pagination;

namespace Ecommerce.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuarios> Incluir(Usuarios usuario);
        Task<Usuarios> Alterar(Usuarios usuario);
        Task<Usuarios> Excluir(int id);
        Task<Usuarios> SelecionarAsync(int id);
        Task<Usuarios> SelecionarPorEmailAsync(string email);
        Task<PagedList<Usuarios>> SelecionarTodosAsync(int pageNumber, int pageSize);
        Task<int> BuscarUltimoId();
    }
}
