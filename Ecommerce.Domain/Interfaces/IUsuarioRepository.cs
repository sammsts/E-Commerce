using Ecommerce.Domain.Entities;

namespace Ecommerce.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuarios> Incluir(Usuarios usuario);
        Task<Usuarios> Alterar(Usuarios usuario);
        Task<Usuarios> Excluir(int id);
        Task<Usuarios> SelecionarAsync(int id);
        Task<IEnumerable<Usuarios>> SelecionarTodosAsync();
        Task<int> BuscarUltimoId();
    }
}
