using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Dto;
using Ecommerce.API.Models;
using Ecommerce.Domain.Account;
using Microsoft.AspNetCore.Authorization;
using Ecommerce.API.Extensions;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly IAuthenticate _authenticateService;
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        

        public UsuarioController(IUsuarioRepository usuarioRepository, IMapper mapper, IUsuarioService usuarioService, IAuthenticate authenticateService)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _usuarioService = usuarioService;
            _authenticateService = authenticateService;
        }

        [HttpGet("SelecionarTodos")]
        [Authorize]
        public async Task<ActionResult> BuscarUsuarios([FromQuery]PaginationParams paginationParams) 
        {
            var usuariosDto = await _usuarioService.SelecionarTodosAsync(paginationParams.PageNumber, paginationParams.pageSize);
            Response.AddPaginationHeader(new PaginationHeader(usuariosDto.CurrentPage, usuariosDto.PageSize, usuariosDto.TotalCount, usuariosDto.TotalPages));
            return Ok(usuariosDto);
        }

        [HttpGet("SelecionarPorId/{id}")]
        [Authorize]
        public async Task<ActionResult> BuscarUsuarioPorId(int id)
        {
            var usuarioDto = await _usuarioService.SelecionarAsync(id);
            if (usuarioDto == null)
            {
                return NotFound("Usuário não encontrado!");
            }

            return Ok(usuarioDto);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> AutenticarUsuario(LoginModel loginModel)
        {
            var usuarioExiste = await _authenticateService.UserExists(loginModel.Email);
            if (!usuarioExiste)
            {
                return Unauthorized("Usuário não cadastrado.");
            }

            var result = await _authenticateService.AuthenticateAsync(loginModel.Email, loginModel.Password);
            if (!result)
            {
                return Unauthorized("Usuário ou senha inválido.");
            }

            var usuario = await _authenticateService.GetUserByEmail(loginModel.Email);

            var token = _authenticateService.GenerateToken(usuario.Usu_id, usuario.Usu_email);

            return new UserToken
            {
                Token = token,
                //IsAdmin = usuario.IsAdmin,
                //Email = usuario.Email
            };
        }

        [HttpPost("RegistrarUsuario")]
        public async Task<ActionResult<UserToken>> CadastrarUsuario(UsuarioDto usuarioDto)
        {
            if (usuarioDto == null)
            {
                BadRequest("Dados inválidos.");
            }

            var emailExiste = await _authenticateService.UserExists(usuarioDto.Usu_email);

            if (emailExiste)
            {
                return BadRequest("Este e-mail já possui um cadastro.");
            }

            var usuarioDtoIncluido = await _usuarioService.Incluir(usuarioDto);
            if (usuarioDtoIncluido == null)
            {
                return BadRequest("Ocorreu um erro ao cadastrar o usuário!");
            }

            var token = _authenticateService.GenerateToken(usuarioDto.Usu_id, usuarioDto.Usu_email);

            return new UserToken
            {
                Token = token
            };
        }

        [HttpPut("AtualizarUsuario")]
        [Authorize]
        public async Task<ActionResult> AlterarUsuario (UsuarioDto usuarioDto)
        {
            var usuarioDtoAlterado = await _usuarioService.Alterar(usuarioDto);
            if (usuarioDtoAlterado == null) 
            {
                return BadRequest("Ocorreu um erro ao tentar alterar o usuário!");
            }

            return Ok("Usuário alterado com sucesso.");
        }

        [HttpDelete("DeletarUsuario/{id}")]
        [Authorize]
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
