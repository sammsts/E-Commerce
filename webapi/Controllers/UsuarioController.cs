using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Dto;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper, IUsuarioService usuarioService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        [HttpGet("SelecionarTodos")] 
        public async Task<ActionResult<IEnumerable<Usuarios>>> BuscarUsuarios() 
        {
            var usuariosDto = await _usuarioService.SelecionarTodosAsync();
            return Ok(usuariosDto);
        }

        [HttpGet("SelecionarPorId/{id}")]
        public async Task<ActionResult> BuscarUsuarioPorId(int id)
        {
            var usuarioDto = await _usuarioService.SelecionarAsync(id);
            if (usuarioDto == null)
            {
                return NotFound("Usuário não encontrado!");
            }

            return Ok(usuarioDto);
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarUsuario(UsuarioDto usuarioDto)
        {
            var usuarioDtoIncluido = await _usuarioService.Incluir(usuarioDto);
            if (usuarioDtoIncluido == null)
            {
                return BadRequest("Ocorreu um erro ao incluir o usuário!");
            }

            return Ok("Usuário cadastrado com sucesso.");
        }

        [HttpPut]
        public async Task<ActionResult> AlterarUsuario (UsuarioDto usuarioDto)
        {
            var usuarioDtoAlterado = await _usuarioService.Alterar(usuarioDto);
            if (usuarioDtoAlterado == null) 
            {
                return BadRequest("Ocorreu um erro ao tentar alterar o usuário!");
            }

            return Ok("Usuário alterado com sucesso.");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ExcluirUsuario (int id)
        {
            var usuarioDtoExcluido = await _usuarioService.Excluir(id);
            if (usuarioDtoExcluido == null)
            {
                return BadRequest("Ocorreu um erro ao tentar excluir o usuário!");
            }

            return Ok("Usuário excluído com sucesso.");
        }
    }
}
