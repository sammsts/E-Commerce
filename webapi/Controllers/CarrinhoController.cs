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
    public class CarrinhoController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ICarrinhoService _carrinhoService;
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IMapper _mapper;

        public CarrinhoController(IUsuarioService usuarioService, ICarrinhoService carrinhoService, ICarrinhoRepository carrinhoRepository, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _carrinhoService = carrinhoService;
            _carrinhoRepository = carrinhoRepository;
            _mapper = mapper;
        }

        [HttpGet("SelecionarTodos")]
        [Authorize]
        public async Task<ActionResult> BuscarCarrinhos([FromQuery] PaginationParams paginationParams)
        {
            var carrinhosDto = await _carrinhoService.SelecionarTodosAsync(paginationParams.PageNumber, paginationParams.pageSize);
            Response.AddPaginationHeader(new PaginationHeader(carrinhosDto.CurrentPage, carrinhosDto.PageSize, carrinhosDto.TotalCount, carrinhosDto.TotalPages));
            return Ok(carrinhosDto);
        }

        [HttpGet("SelecionarPorId/{id}")]
        [Authorize]
        public async Task<ActionResult> BuscarCarrinhoPorId(int id)
        {
            var carrinhoDto = await _carrinhoService.SelecionarAsync(id);
            if (carrinhoDto == null)
            {
                return NotFound("Carrinho não encontrado!");
            }

            return Ok(carrinhoDto);
        }

        [HttpGet("SelecionarCarrinhoAberto")]
        [Authorize]
        public async Task<ActionResult> BuscarCarrinhoAberto([FromQuery] CarrinhoAbertoDto obj)
        {
            var carrinhoDto = await _carrinhoService.SelecionarCarrinhoAbertoAsync(obj);
            if (carrinhoDto == null)
            {
                return NotFound("Carrinho não encontrado!");
            }

            return Ok(carrinhoDto);
        }

        [HttpPost("RegistrarCarrinho")]
        [Authorize]
        public async Task<ActionResult> CadastrarCarrinho(CarrinhoDto carrinhoDto)
        {
            var isAdmin = await PermissionsAdmin();

            if (!isAdmin)
            {
                return Unauthorized("Você não tem permissão para consultar carrinho.");
            }

            if (carrinhoDto == null)
            {
                BadRequest("Dados inválidos.");
            }

            var carrinhoDtoIncluido = await _carrinhoService.Incluir(carrinhoDto);
            if (carrinhoDtoIncluido == null)
            {
                return BadRequest("Ocorreu um erro ao cadastrar o carrinho!");
            }

            return Ok("Carrinho cadastrado com sucesso.");
        }

        [HttpPut("AtualizarCarrinho")]
        [Authorize]
        public async Task<ActionResult> AlterarCarrinho(CarrinhoDto carrinhoDto)
        {
            var carrinhoDtoAlterado = await _carrinhoService.Alterar(carrinhoDto);
            if (carrinhoDtoAlterado == null)
            {
                return BadRequest("Ocorreu um erro ao tentar alterar o carrinho!");
            }

            return Ok("Carrinho alterado com sucesso.");
        }

        [HttpDelete("DeletarCarrinho/{serialId}")]
        [Authorize]
        public async Task<ActionResult> ExcluirCarrinho(int serialId)
        {
            var isAdmin = await PermissionsAdmin();

            if (!isAdmin)
            {
                return Unauthorized("Você não tem permissão para excluir carrinhos.");
            }

            var carrinhoDtoExcluido = await _carrinhoService.Excluir(serialId);
            if (carrinhoDtoExcluido == null)
            {
                return BadRequest("Ocorreu um erro ao tentar excluir o carrinho!");
            }

            return Ok("Carrinho excluído com sucesso.");
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
