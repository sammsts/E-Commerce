using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Pagination;
using Ecommerce.Infra.Data.Context;
using Ecommerce.Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Ecommerce.Infra.Data.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly EcommerceContext _context;

        public CarrinhoRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<Carrinho> Alterar(Carrinho carrinho)
        {
            try
            {
                _context.Carrinho.Update(carrinho);
                await _context.SaveChangesAsync();
                return carrinho;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar alterar o carrinho.", ex);
            }
        }

        public async Task<Carrinho> AlterarSituacaoCarrinho(int id)
        {
            try
            {
                var carrinho = await _context.Carrinho.FirstOrDefaultAsync(x => x.Car_Id == id);

                if (carrinho != null)
                {
                    carrinho.Car_Situacao = SituacaoCarrinho.Fechado;

                    _context.Carrinho.Update(carrinho);
                    await _context.SaveChangesAsync();
                }

                return carrinho;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar alterar o carrinho.", ex);
            }
        }

        public async Task<Carrinho> Excluir(int id)
        {
            try
            {
                var carrinho = await _context.Carrinho.Where(x => x.Prd_Id == id).FirstOrDefaultAsync();
                if (carrinho != null)
                {
                    _context.Carrinho.Remove(carrinho);
                    await _context.SaveChangesAsync();
                }
                return carrinho;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar excluir o carrinho.", ex);
            }
        }

        public async Task<Carrinho> Incluir(Carrinho carrinho)
        {
            try
            {
                _context.Carrinho.Add(carrinho);
                await _context.SaveChangesAsync();
                return carrinho;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar incluir o carrinho.", ex);
            }
        }

        public async Task<Carrinho> SelecionarAsync(int id)
        {
            try
            {
                return await _context.Carrinho.Where(x => x.Car_Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocorreu um erro ao buscar o carrinho.", ex);
            }
        }

        public async Task<IEnumerable<Carrinho>> SelecionarCarrinhoAbertoAsync(int Usu_Id, int End_Id)
        {
            try
            {
                return await _context.Carrinho.Where(x => x.Usu_Id == Usu_Id && x.End_Id == End_Id && x.Car_Situacao == 0).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocorreu um erro ao buscar o carrinho.", ex);
            }
        }

        public async Task<PagedList<Carrinho>> SelecionarTodosAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = _context.Carrinho.AsQueryable();

                return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocorreu um erro ao buscar os carrinhos.", ex);
            }
        }
    }
}
