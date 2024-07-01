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
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;
        private readonly ICarrinhoRepository _repositoryCar;
        private readonly IMapper _mapper;

        public PedidoService(IPedidoRepository repository, ICarrinhoRepository repositoryCar, IMapper mapper)
        {
            _repository = repository;
            _repositoryCar = repositoryCar;
            _mapper = mapper;
        }

        public async Task<PedidoDto> Alterar(PedidoDto pedidoDto)
        {
            try
            {
                var alterarPedido = await _repository.SelecionarAsync((int)pedidoDto.Ped_Id);

                if (alterarPedido == null)
                {
                    throw new NotFoundException("Pedido não encontrado");
                }

                var pedido = _mapper.Map<Pedidos>(alterarPedido);
                var pedidoAlterado = await _repository.Alterar(pedido);
                return _mapper.Map<PedidoDto>(pedidoAlterado);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ServiceException("Ocorreu um erro ao tentar alterar o pedidos.", ex);
            }
        }

        public async Task<PedidoDto> Excluir(int id)
        {
            try
            {
                var pedidoExcluido = await _repository.Excluir(id);
                if (pedidoExcluido == null)
                {
                    throw new NotFoundException($"Usuário não encontrado.");
                }
                return _mapper.Map<PedidoDto>(pedidoExcluido);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ServiceException("Ocorreu um erro ao tentar excluir o pedidos.", ex);
            }
        }

        public async Task<PedidoDto> Incluir(PedidoDto pedidoDto)
        {
            try
            {
                pedidoDto.Ped_DataPedido = DateTime.UtcNow;

                var pedido = _mapper.Map<Pedidos>(pedidoDto);
                var pedidoIncluido = await _repository.Incluir(pedido);

                var carrinhoAberto = await _repositoryCar.SelecionarCarrinhoAbertoAsync(pedidoDto.Usu_Id, pedidoDto.End_Id);

                foreach (var car in carrinhoAberto)
                {
                    var fechaCarrinho = await _repositoryCar.AlterarSituacaoCarrinho(car.Car_Id);
                }

                return _mapper.Map<PedidoDto>(pedidoIncluido);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ServiceException("Ocorreu um erro ao tentar incluir o pedido.", ex);
            }
        }

        public async Task<PedidoDto> SelecionarAsync(int id)
        {
            try
            {
                var pedido = await _repository.SelecionarAsync(id);
                return _mapper.Map<PedidoDto>(pedido);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Ocorreu um erro ao tentar buscar o pedidos.", ex);
            }
        }

        public async Task<PagedList<PedidoDto>> SelecionarTodosAsync(int pageNumber, int pageSize)
        {
            try
            {
                var pedidos = await _repository.SelecionarTodosAsync(pageNumber, pageSize);
                var pedidosDto = _mapper.Map<IEnumerable<PedidoDto>>(pedidos);
                return new PagedList<PedidoDto>(pedidosDto, pageNumber, pageSize, pedidos.TotalCount);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Ocorreu um erro ao tentar buscar os pedidos.", ex);
            }
        }
    }
}
