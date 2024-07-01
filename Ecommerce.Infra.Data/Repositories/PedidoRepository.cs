using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Pagination;
using Ecommerce.Infra.Data.Context;
using Ecommerce.Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Ecommerce.Infra.Data.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly EcommerceContext _context;

        public PedidoRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<Pedidos> Alterar(Pedidos pedido)
        {
            try
            {
                _context.Pedidos.Update(pedido);
                await _context.SaveChangesAsync();
                return pedido;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar alterar o pedido.", ex);
            }
        }

        public async Task<Pedidos> Excluir(int id)
        {
            try
            {
                var pedido = await _context.Pedidos.Where(x => x.Ped_Id == id).FirstOrDefaultAsync();
                if (pedido != null)
                {
                    _context.Pedidos.Remove(pedido);
                    await _context.SaveChangesAsync();
                }
                return pedido;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar excluir o pedido.", ex);
            }
        }

        public async Task<Pedidos> Incluir(Pedidos pedido)
        {
            try
            {
                _context.Pedidos.Add(pedido);
                await _context.SaveChangesAsync();
                return pedido;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar incluir o pedido.", ex);
            }
        }

        public async Task<Pedidos> SelecionarAsync(int id)
        {
            try
            {
                return await _context.Pedidos.Where(x => x.Ped_Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocorreu um erro ao buscar o pedido.", ex);
            }
        }

        public async Task<PagedList<Pedidos>> SelecionarTodosAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = _context.Pedidos.AsQueryable();

                return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocorreu um erro ao buscar os pedidos.", ex);
            }
        }
    }
}
