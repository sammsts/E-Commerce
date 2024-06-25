using Ecommerce.API.Models;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Interfaces.Repositorios;

namespace webapi.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly EcommerceContext _context;

        public UsuarioRepository(EcommerceContext context)
        {
            _context = context;
        }

        public void Alterar(Usuarios usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
        }

        public void Excluir(Usuarios usuario)
        {
            _context.Usuarios.Remove(usuario);
        }

        public void Incluir(Usuarios usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public async Task<bool> SalvarTodosAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Usuarios> SelecionarPorId(int id)
        {
            return await _context.Usuarios.Where(x => x.Usu_id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Usuarios>> SelecionarTodos()
        {
            return await _context.Usuarios.ToListAsync();
        }
    }
}
