using AutoMapper;
using Ecommerce.API.Extensions;
using Ecommerce.API.Models;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Infra.Ioc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPedidoService _pedidoService;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public PedidoController(IUsuarioService usuarioService, IPedidoService pedidoService, IPedidoRepository pedidoRepository, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _pedidoService = pedidoService;
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        [HttpGet("SelecionarTodos")]
        [Authorize]
        public async Task<ActionResult> BuscarPedidos([FromQuery] PaginationParams paginationParams)
        {
            var pedidosDto = await _pedidoService.SelecionarTodosAsync(paginationParams.PageNumber, paginationParams.pageSize);
            Response.AddPaginationHeader(new PaginationHeader(pedidosDto.CurrentPage, pedidosDto.PageSize, pedidosDto.TotalCount, pedidosDto.TotalPages));
            return Ok(pedidosDto);
        }

        [HttpGet("SelecionarPorId/{id}")]
        [Authorize]
        public async Task<ActionResult> BuscarPedidoPorId(int id)
        {
            var pedidoDto = await _pedidoService.SelecionarAsync(id);
            if (pedidoDto == null)
            {
                return NotFound("Pedido não encontrado!");
            }

            return Ok(pedidoDto);
        }

        [HttpPost("RegistrarPedido")]
        [Authorize]
        public async Task<ActionResult> CadastrarPedido(PedidoDto pedidoDto)
        {
            var isAdmin = await PermissionsAdmin();

            if (!isAdmin)
            {
                return Unauthorized("Você não tem permissão para registrar pedido.");
            }

            if (pedidoDto == null)
            {
                BadRequest("Dados inválidos.");
            }

            var pedidoDtoIncluido = await _pedidoService.Incluir(pedidoDto);
            if (pedidoDtoIncluido == null)
            {
                return BadRequest("Ocorreu um erro ao cadastrar o pedido!");
            }

            return Ok("Pedido cadastrado com sucesso.");
        }

        [HttpPut("AtualizarPedido")]
        [Authorize]
        public async Task<ActionResult> AlterarPedido(PedidoDto pedidoDto)
        {
            var pedidoDtoAlterado = await _pedidoService.Alterar(pedidoDto);
            if (pedidoDtoAlterado == null)
            {
                return BadRequest("Ocorreu um erro ao tentar alterar o pedido!");
            }

            return Ok("Pedido alterado com sucesso.");
        }

        [HttpDelete("DeletarPedido/{serialId}")]
        [Authorize]
        public async Task<ActionResult> ExcluirPedido(int id)
        {
            var isAdmin = await PermissionsAdmin();

            if (!isAdmin)
            {
                return Unauthorized("Você não tem permissão para excluir pedidos.");
            }

            var pedidoDtoExcluido = await _pedidoService.Excluir(id);
            if (pedidoDtoExcluido == null)
            {
                return BadRequest("Ocorreu um erro ao tentar excluir o pedido!");
            }

            return Ok("Pedido excluído com sucesso.");
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
