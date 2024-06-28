using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Pagination;

namespace Ecommerce.Domain.Interfaces
{
    public interface IEnderecoRepository
    {
        Task<Enderecos> Incluir(Enderecos endereco);
        Task<Enderecos> Alterar(Enderecos endereco);
        Task<Enderecos> Excluir(int id);
        Task<Enderecos> SelecionarAsync(int Usu_id);
        Task<PagedList<Enderecos>> SelecionarTodosAsync(int pageNumber, int pageSize);
        Task<int> BuscarUltimoId();
    }
}
