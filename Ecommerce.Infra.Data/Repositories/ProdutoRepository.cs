using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Pagination;
using Ecommerce.Infra.Data.Context;
using Ecommerce.Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Ecommerce.Infra.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly EcommerceContext _context;

        public ProdutoRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<Produtos> Alterar(Produtos produto)
        {
            try
            {
                _context.Produtos.Update(produto);
                await _context.SaveChangesAsync();
                return produto;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar alterar o produto.", ex);
            }
        }

        public async Task<Produtos> Excluir(int serialId)
        {
            try
            {
                var produto = await _context.Produtos.Where(x => x.Prd_serialId == serialId).FirstOrDefaultAsync();
                if (produto != null)
                {
                    _context.Produtos.Remove(produto);
                    await _context.SaveChangesAsync();
                }
                return produto;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar excluir o produto.", ex);
            }
        }

        public async Task<Produtos> Incluir(Produtos produto)
        {
            try
            {
                _context.Produtos.Add(produto);
                await _context.SaveChangesAsync();
                return produto;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar incluir o produto.", ex);
            }
        }

        public async Task<Produtos> SelecionarAsync(int serialId)
        {
            try
            {
                return await _context.Produtos.Where(x => x.Prd_serialId == serialId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocorreu um erro ao buscar o produto.", ex);
            }
        }

        public async Task<PagedList<Produtos>> SelecionarTodosAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = _context.Produtos.AsQueryable();

                return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocorreu um erro ao buscar os produtos.", ex);
            }
        }
    }
}
