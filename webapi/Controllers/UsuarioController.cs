using AutoMapper;
using Ecommerce.API.Dto;
using Ecommerce.API.Models;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Interfaces.Repositorios;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        [HttpGet("SelecionarTodos")] 
        public async Task<ActionResult<IEnumerable<Usuarios>>> BuscarUsuarios() 
        {
            var usuarios = await _usuarioRepository.SelecionarTodos();
            var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
            return Ok(usuariosDto);
        }

        [HttpGet("SelecionarPorId/{id}")]
        public async Task<ActionResult> BuscarUsuarioPorId(int id)
        {
            var usuario = await _usuarioRepository.SelecionarPorId(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado!");
            }

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return Ok(usuarioDto);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarUsuario(UsuarioDto usuarioDto)
        {
            var usuario = _mapper.Map<Usuarios>(usuarioDto);
            _usuarioRepository.Incluir(usuario);

            if (await _usuarioRepository.SalvarTodosAsync())
            {
                return Ok("Usuário cadastrado com sucesso!");
            }

            return BadRequest("Ocorreu um erro ao salvar o usuário.");
        }

        [HttpPut]
        public async Task<ActionResult> AlterarUsuario (UsuarioDto usuarioDto)
        {
            if (usuarioDto.Usu_id == 0)
            {
                return BadRequest("Não é possível alterar o usuário. É preciso informar o ID.");
            }

            var usuarioExiste = await _usuarioRepository.SelecionarPorId(usuarioDto.Usu_id);

            if (usuarioExiste == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var usuario = _mapper.Map<Usuarios>(usuarioDto);
            _usuarioRepository.Alterar(usuario);

            if (await _usuarioRepository.SalvarTodosAsync())
            {
                return Ok("Usuário alterado com sucesso!");
            }

            return BadRequest("Ocorreu um erro ao alterar o usuário.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirUsuario (int id)
        {
            var usuario = await _usuarioRepository.SelecionarPorId(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            _usuarioRepository.Excluir(usuario);

            if (await _usuarioRepository.SalvarTodosAsync())
            {
                return Ok("Usuário excluído com sucesso!");
            }

            return BadRequest("Ocorreu um erro ao excluir o usuário.");
        }
    }
}
