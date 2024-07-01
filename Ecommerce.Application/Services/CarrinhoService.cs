using Amazon.Lambda.Model;
using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Pagination;
using OpenQA.Selenium;

namespace Ecommerce.Application.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ICarrinhoRepository _repository;
        private readonly IMapper _mapper;

        public CarrinhoService(ICarrinhoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<CarrinhoDto> Alterar(CarrinhoDto carrinhoDto)
        {
            try
            {
                var alterarCarrinho = await _repository.SelecionarAsync((int)carrinhoDto.Car_Id);

                if (alterarCarrinho == null)
                {
                    throw new NotFoundException("Carrinho não encontrado");
                }

                var carrinho = _mapper.Map<Carrinho>(alterarCarrinho);
                var carrinhoAlterado = await _repository.Alterar(carrinho);
                return _mapper.Map<CarrinhoDto>(carrinhoAlterado);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ServiceException("Ocorreu um erro ao tentar alterar o carrinhos.", ex);
            }
        }

        public async Task<CarrinhoDto> Excluir(int serialId)
        {
            try
            {
                var carrinhoExcluido = await _repository.Excluir(serialId);
                if (carrinhoExcluido == null)
                {
                    throw new NotFoundException($"Usuário não encontrado.");
                }
                return _mapper.Map<CarrinhoDto>(carrinhoExcluido);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ServiceException("Ocorreu um erro ao tentar excluir o carrinhos.", ex);
            }
        }

        public async Task<CarrinhoDto> Incluir(CarrinhoDto carrinhoDto)
        {
            try
            {
                var carrinho = _mapper.Map<Carrinho>(carrinhoDto);
                var carrinhoIncluido = await _repository.Incluir(carrinho);
                return _mapper.Map<CarrinhoDto>(carrinhoIncluido);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ServiceException("Ocorreu um erro ao tentar incluir o carrinho.", ex);
            }
        }

        public async Task<CarrinhoDto> SelecionarAsync(int serialId)
        {
            try
            {
                var carrinho = await _repository.SelecionarAsync(serialId);
                return _mapper.Map<CarrinhoDto>(carrinho);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Ocorreu um erro ao tentar buscar o carrinhos.", ex);
            }
        }

        public async Task<IEnumerable<CarrinhoDto>> SelecionarCarrinhoAbertoAsync(CarrinhoAbertoDto obj)
        {
            try
            {
                var carrinho = await _repository.SelecionarCarrinhoAbertoAsync(obj.Usu_id, obj.End_id);
                return _mapper.Map<IEnumerable<CarrinhoDto>>(carrinho);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Ocorreu um erro ao tentar buscar o carrinhos.", ex);
            }
        }

        public async Task<PagedList<CarrinhoDto>> SelecionarTodosAsync(int pageNumber, int pageSize)
        {
            try
            {
                var carrinhos = await _repository.SelecionarTodosAsync(pageNumber, pageSize);
                var carrinhosDto = _mapper.Map<IEnumerable<CarrinhoDto>>(carrinhos);
                return new PagedList<CarrinhoDto>(carrinhosDto, pageNumber, pageSize, carrinhos.TotalCount);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Ocorreu um erro ao tentar buscar os carrinhos.", ex);
            }
        }
    }
}
