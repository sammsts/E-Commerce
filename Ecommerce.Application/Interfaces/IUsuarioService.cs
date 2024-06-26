using Ecommerce.Application.Dto;

namespace Ecommerce.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDto> Incluir(UsuarioDto usuarioDto);
        Task<UsuarioDto> Alterar(UsuarioDto usuarioDto);
        Task<UsuarioDto> Excluir(int id);
        Task<UsuarioDto> SelecionarAsync(int id);
        Task<IEnumerable<UsuarioDto>> SelecionarTodosAsync();
    }
}
