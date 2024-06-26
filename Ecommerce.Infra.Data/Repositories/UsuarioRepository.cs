using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Ecommerce.Interfaces.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly EcommerceContext _context;

        public UsuarioRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<Usuarios> Alterar(Usuarios usuario)
        {
            try
            {
                _context.Usuarios.Update(usuario);
                await _context.SaveChangesAsync();
                return usuario;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar alterar o usuário.", ex);
            }
        }

        public async Task<Usuarios> Excluir(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario != null)
                {
                    _context.Usuarios.Remove(usuario);
                    await _context.SaveChangesAsync();
                }
                return usuario;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar excluir o usuário.", ex);
            }
        }

        public async Task<Usuarios> Incluir(Usuarios usuario)
        {
            try
            {
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
                return usuario;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar incluir o usuário.", ex);
            }
        }

        public async Task<Usuarios> SelecionarAsync(int id)
        {
            try
            {
                return await _context.Usuarios.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocorreu um erro ao buscar o usuário.", ex);
            }
        }

        public async Task<IEnumerable<Usuarios>> SelecionarTodosAsync()
        {
            try
            {
                return await _context.Usuarios.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocorreu um erro ao buscar os usuários.", ex);
            }
        }

        public async Task<int> BuscarUltimoId()
        {
            try
            {
                return await _context.Usuarios.MaxAsync(x => x.Usu_id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocorreu um erro ao buscar último usuário salvo.", ex);
            }
        }
    }
}
