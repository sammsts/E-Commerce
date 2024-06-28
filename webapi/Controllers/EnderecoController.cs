using AutoMapper;
using Ecommerce.API.Extensions;
using Ecommerce.API.Models;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Account;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Infra.Ioc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : Controller
    {
        private readonly IAuthenticate _authenticateService;
        private readonly IUsuarioService _usuarioService;
        private readonly IEnderecoService _enderecoService;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;

        public EnderecoController(IAuthenticate authenticateService, IUsuarioService usuarioService, IEnderecoService enderecoService, IEnderecoRepository enderecoRepository, IMapper mapper)
        {
            _authenticateService = authenticateService;
            _usuarioService = usuarioService;
            _enderecoService = enderecoService;
            _enderecoRepository = enderecoRepository;
            _mapper = mapper;
        }

        [HttpGet("SelecionarTodos")]
        [Authorize]
        public async Task<ActionResult> BuscarEnderecos([FromQuery] PaginationParams paginationParams)
        {
            var isAdmin = await PermissionsAdmin();

            if (!isAdmin)
            {
                return Unauthorized("Você não tem permissão para consultar endereços.");
            }

            var enderecosDto = await _enderecoService.SelecionarTodosAsync(paginationParams.PageNumber, paginationParams.pageSize);
            Response.AddPaginationHeader(new PaginationHeader(enderecosDto.CurrentPage, enderecosDto.PageSize, enderecosDto.TotalCount, enderecosDto.TotalPages));
            return Ok(enderecosDto);
        }

        [HttpGet("SelecionarPorId/{id}")]
        [Authorize]
        public async Task<ActionResult> BuscarEnderecoPorId(int id)
        {
            var enderecoDto = await _enderecoService.SelecionarAsync(id);
            if (enderecoDto == null)
            {
                return NotFound("Endereço não encontrado!");
            }

            return Ok(enderecoDto);
        }

        [HttpPost("RegistrarEndereco")]
        [Authorize]
        public async Task<ActionResult> CadastrarEndereco(EnderecoDto enderecoDto)
        {
            if (enderecoDto == null)
            {
                BadRequest("Dados inválidos.");
            }

            var enderecoExiste = await _enderecoService.SelecionarAsync(User.GetId());
            enderecoDto.Usu_id = User.GetId();

            if (enderecoExiste == null)
            {
                var enderecoDtoIncluido = await _enderecoService.Incluir(enderecoDto);
                if (enderecoDtoIncluido == null)
                {
                    return BadRequest("Ocorreu um erro ao cadastrar o endereço!");
                }

                return Ok("Endereço incluído com sucesso.");
            }

            var enderecoDtoAlterado = await _enderecoService.Alterar(enderecoDto);
            if (enderecoDtoAlterado == null)
            {
                return BadRequest("Ocorreu um erro ao tentar alterar o endereço!");
            }

            return Ok("Endereço alterado com sucesso.");
        }

        [HttpPut("AtualizarUsuario")]
        [Authorize]
        public async Task<ActionResult> AlterarEndereco(EnderecoDto enderecoDto)
        {
            var enderecoDtoAlterado = await _enderecoService.Alterar(enderecoDto);
            if (enderecoDtoAlterado == null)
            {
                return BadRequest("Ocorreu um erro ao tentar alterar o endereço!");
            }

            return Ok("Endereço alterado com sucesso.");
        }

        [HttpDelete("DeletarEndereco/{id}")]
        [Authorize]
        public async Task<ActionResult> ExcluirEndereco(int id)
        {
            var isAdmin = await PermissionsAdmin();

            if (!isAdmin)
            {
                return Unauthorized("Você não tem permissão para excluir endereços.");
            }

            var enderecoDtoExcluido = await _enderecoService.Excluir(id);
            if (enderecoDtoExcluido == null)
            {
                return BadRequest("Ocorreu um erro ao tentar excluir o endereço!");
            }

            return Ok("Usuário excluído com sucesso.");
        }

        private async Task<bool> PermissionsAdmin()
        {
            var userId = User.GetId();
            var usuario = await _usuarioService.SelecionarAsync(userId);

            if (usuario != null)
            {
                if (usuario.Usu_IsAdmin)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
