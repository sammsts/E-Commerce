using AutoMapper;
using Ecommerce.API.Extensions;
using Ecommerce.API.Models;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Services;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Infra.Data.Identity;
using Ecommerce.Infra.Ioc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IProdutoService _produtoService;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoController(IUsuarioService usuarioService, IProdutoService produtoService, IProdutoRepository produtoRepository, IMapper mapper)
        {
            _usuarioService = usuarioService;
            _produtoService = produtoService;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        [HttpGet("SelecionarTodos")]
        [Authorize]
        public async Task<ActionResult> BuscarProdutos([FromQuery] PaginationParams paginationParams)
        {
            var produtosDto = await _produtoService.SelecionarTodosAsync(paginationParams.PageNumber, paginationParams.pageSize);
            Response.AddPaginationHeader(new PaginationHeader(produtosDto.CurrentPage, produtosDto.PageSize, produtosDto.TotalCount, produtosDto.TotalPages));
            return Ok(produtosDto);
        }

        [HttpGet("SelecionarPorId/{id}")]
        [Authorize]
        public async Task<ActionResult> BuscarProdutoPorId(int id)
        {
            var produtoDto = await _produtoService.SelecionarAsync(id);
            if (produtoDto == null)
            {
                return NotFound("Produto não encontrado!");
            }

            return Ok(produtoDto);
        }

        [HttpPost("RegistrarProduto")]
        [Authorize]
        public async Task<ActionResult> CadastrarProduto(ProdutoDto produtoDto)
        {
            var isAdmin = await PermissionsAdmin();

            if (!isAdmin)
            {
                return Unauthorized("Você não tem permissão para consultar produto.");
            }

            if (produtoDto == null)
            {
                BadRequest("Dados inválidos.");
            }

            var produtoExiste = await _produtoService.SelecionarAsync(produtoDto.Prd_serialId);

            if (produtoExiste != null)
            {
                return BadRequest("Este produto já possui um cadastro.");
            }

            var produtoDtoIncluido = await _produtoService.Incluir(produtoDto);
            if (produtoDtoIncluido == null)
            {
                return BadRequest("Ocorreu um erro ao cadastrar o produto!");
            }

            return Ok("Produto cadastrado com sucesso.");
        }

        [HttpPut("AtualizarProduto")]
        [Authorize]
        public async Task<ActionResult> AlterarProduto(ProdutoDto produtoDto)
        {
            var produtoDtoAlterado = await _produtoService.Alterar(produtoDto);
            if (produtoDtoAlterado == null)
            {
                return BadRequest("Ocorreu um erro ao tentar alterar o produto!");
            }

            return Ok("Produto alterado com sucesso.");
        }

        [HttpDelete("DeletarProduto/{serialId}")]
        [Authorize]
        public async Task<ActionResult> ExcluirProduto(int serialId)
        {
            var isAdmin = await PermissionsAdmin();

            if (!isAdmin)
            {
                return Unauthorized("Você não tem permissão para excluir produtos.");
            }

            var produtoDtoExcluido = await _produtoService.Excluir(serialId);
            if (produtoDtoExcluido == null)
            {
                return BadRequest("Ocorreu um erro ao tentar excluir o produto!");
            }

            return Ok("Produto excluído com sucesso.");
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
