using Ecommerce.Application.DTOs;
using Ecommerce.Domain.Pagination;

namespace Ecommerce.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> Incluir(UsuarioDto usuarioDto);
        Task<UsuarioDto> Alterar(UsuarioDto usuarioDto);
        Task<UsuarioDto> Excluir(int id);
        Task<UsuarioDto> SelecionarAsync(int id);
        Task<UsuarioDto> SelecionarPorEmailAsync(string email);
        Task<PagedList<UsuarioDto>> SelecionarTodosAsync(int pageNumber, int pageSize);
    }
}
