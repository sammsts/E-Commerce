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
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _repository;
        private readonly IMapper _mapper;

        public EnderecoService(IEnderecoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<EnderecoDto> Alterar(EnderecoDto enderecoDto)
        {
            try
            {
                var endereco = _mapper.Map<Enderecos>(enderecoDto);
                var enderecoAlterado = await _repository.Alterar(endereco);
                return _mapper.Map<EnderecoDto>(enderecoAlterado);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ServiceException("Ocorreu um erro ao tentar alterar o endereço.", ex);
            }
        }

        public async Task<EnderecoDto> Excluir(int id)
        {
            try
            {
                var enderecoExcluido = await _repository.Excluir(id);
                if (enderecoExcluido == null)
                {
                    throw new NotFoundException($"Usuário não encontrado.");
                }
                return _mapper.Map<EnderecoDto>(enderecoExcluido);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ServiceException("Ocorreu um erro ao tentar excluir o endereço.", ex);
            }
        }

        public async Task<EnderecoDto> Incluir(EnderecoDto enderecoDto)
        {
            try
            {
                var endereco = _mapper.Map<Enderecos>(enderecoDto);
                var enderecoIncluido = await _repository.Incluir(endereco);
                return _mapper.Map<EnderecoDto>(enderecoIncluido);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ServiceException("Ocorreu um erro ao tentar incluir o endereço.", ex);
            }
        }

        public async Task<EnderecoDto> SelecionarAsync(int Usu_id)
        {
            try
            {
                var endereco = await _repository.SelecionarAsync(Usu_id);
                return _mapper.Map<EnderecoDto>(endereco);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Ocorreu um erro ao tentar buscar o endereço.", ex);
            }
        }

        public async Task<PagedList<EnderecoDto>> SelecionarTodosAsync(int pageNumber, int pageSize)
        {
            try
            {
                var enderecos = await _repository.SelecionarTodosAsync(pageNumber, pageSize);
                var enderecosDto = _mapper.Map<IEnumerable<EnderecoDto>>(enderecos);
                return new PagedList<EnderecoDto>(enderecosDto, pageNumber, pageSize, enderecos.TotalCount);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Ocorreu um erro ao tentar buscar os endereços.", ex);
            }
        }
    }
}
