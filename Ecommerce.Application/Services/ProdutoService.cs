using Amazon.Lambda.Model;
using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Pagination;
using Microsoft.AspNetCore.Http.HttpResults;
using OpenQA.Selenium;

namespace Ecommerce.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProdutoDto> Alterar(ProdutoDto produtoDto)
        {
            try
            {
                var alterarProduto = await _repository.SelecionarAsync(produtoDto.Prd_serialId);

                if (alterarProduto == null)
                {
                    throw new NotFoundException("Produto não encontrado");
                }

                alterarProduto.Prd_descricao = produtoDto.Prd_descricao;
                alterarProduto.Prd_quantidadeEstoque = produtoDto.Prd_quantidadeEstoque;
                alterarProduto.Prd_dataHoraCadastro = DateTime.UtcNow;

                var produto = _mapper.Map<Produtos>(alterarProduto);
                var produtoAlterado = await _repository.Alterar(produto);
                return _mapper.Map<ProdutoDto>(produtoAlterado);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ServiceException("Ocorreu um erro ao tentar alterar o produtos.", ex);
            }
        }

        public async Task<ProdutoDto> Excluir(int serialId)
        {
            try
            {
                var produtoExcluido = await _repository.Excluir(serialId);
                if (produtoExcluido == null)
                {
                    throw new NotFoundException($"Usuário não encontrado.");
                }
                return _mapper.Map<ProdutoDto>(produtoExcluido);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ServiceException("Ocorreu um erro ao tentar excluir o produtos.", ex);
            }
        }

        public async Task<ProdutoDto> Incluir(ProdutoDto produtoDto)
        {
            try
            {
                produtoDto.Prd_dataHoraCadastro = DateTime.UtcNow;

                var produto = _mapper.Map<Produtos>(produtoDto);
                var produtoIncluido = await _repository.Incluir(produto);
                return _mapper.Map<ProdutoDto>(produtoIncluido);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ServiceException("Ocorreu um erro ao tentar incluir o produto.", ex);
            }
        }

        public async Task<ProdutoDto> SelecionarAsync(int serialId)
        {
            try
            {
                var produto = await _repository.SelecionarAsync(serialId);
                return _mapper.Map<ProdutoDto>(produto);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Ocorreu um erro ao tentar buscar o produtos.", ex);
            }
        }

        public async Task<PagedList<ProdutoDto>> SelecionarTodosAsync(int pageNumber, int pageSize)
        {
            try
            {
                var produtos = await _repository.SelecionarTodosAsync(pageNumber, pageSize);
                var produtosDto = _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
                return new PagedList<ProdutoDto>(produtosDto, pageNumber, pageSize, produtos.TotalCount);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Ocorreu um erro ao tentar buscar os produtos.", ex);
            }
        }
    }
}
