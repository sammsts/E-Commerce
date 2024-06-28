using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Pagination;
using Ecommerce.Infra.Data.Context;
using Ecommerce.Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Ecommerce.Infra.Data.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly EcommerceContext _context;

        public EnderecoRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<Enderecos> Alterar(Enderecos endereco)
        {
            try
            {
                var enderecoAlterar = await SelecionarAsync(endereco.Usu_id);

                enderecoAlterar.End_cep = endereco.End_cep;
                enderecoAlterar.End_rua = endereco.End_rua;
                enderecoAlterar.End_pais = endereco.End_pais;
                enderecoAlterar.End_bairro = endereco.End_bairro;
                enderecoAlterar.End_estado = endereco.End_estado;
                enderecoAlterar.End_numero = endereco.End_numero;
                enderecoAlterar.End_cidade = endereco.End_cidade;
                enderecoAlterar.End_complemento = endereco.End_complemento;

                _context.Enderecos.Update(enderecoAlterar);
                await _context.SaveChangesAsync();

                return enderecoAlterar;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar alterar o endereço.", ex);
            }
        }

        public async Task<int> BuscarUltimoId()
        {
            try
            {
                return await _context.Enderecos.MaxAsync(x => x.Usu_id);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocorreu um erro ao buscar último endereço salvo.", ex);
            }
        }

        public async Task<Enderecos> Excluir(int id)
        {
            try
            {
                var endereco = await _context.Enderecos.FindAsync(id);
                if (endereco != null)
                {
                    _context.Enderecos.Remove(endereco);
                    await _context.SaveChangesAsync();
                }
                return endereco;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar excluir o endereço.", ex);
            }
        }

        public async Task<Enderecos> Incluir(Enderecos endereco)
        {
            try
            {
                _context.Enderecos.Add(endereco);
                await _context.SaveChangesAsync();
                return endereco;
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new RepositoryException("Ocorreu um erro ao tentar incluir o endereço.", ex);
            }
        }

        public async Task<Enderecos> SelecionarAsync(int Usu_id)
        {
            try
            {
                return await _context.Enderecos.Where(x => x.Usu_id == Usu_id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocorreu um erro ao buscar o endereço.", ex);
            }
        }

        public async Task<PagedList<Enderecos>> SelecionarTodosAsync(int pageNumber, int pageSize)
        {
            try
            {
                var query = _context.Enderecos.AsQueryable();

                return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
            }
            catch (Exception ex)
            {
                throw new RepositoryException("Ocorreu um erro ao buscar os endereços.", ex);
            }
        }
    }
}
