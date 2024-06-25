using Ecommerce.API.Models;

namespace Ecommerce.Interfaces.Repositorios
{
    public interface IUsuarioRepository
    {
        void Incluir(Usuarios usuario);
        void Alterar(Usuarios usuario);
        void Excluir(Usuarios usuario);
        Task<Usuarios> SelecionarPorId(int id);
        Task<IEnumerable<Usuarios>> SelecionarTodos();
        Task<bool> SalvarTodosAsync();
    }
}
