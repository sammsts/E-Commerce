using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Pagination;

namespace Ecommerce.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> Incluir(UsuarioDto usuarioDto);
        Task<AtualizaUsuarioDto> Alterar(AtualizaUsuarioDto usuarioDto);
        Task<UsuarioDto> Excluir(int id);
        Task<UsuarioDto> SelecionarAsync(int id);
        Task<PagedList<UsuarioDto>> SelecionarTodosAsync(int pageNumber, int pageSize);
    }
}
