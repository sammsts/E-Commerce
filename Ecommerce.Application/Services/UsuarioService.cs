using Amazon.Lambda.Model;
using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using OpenQA.Selenium;

namespace Ecommerce.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UsuarioDto> Alterar(UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = _mapper.Map<Usuarios>(usuarioDto);
                var usuarioAlterado = await _repository.Alterar(usuario);
                return _mapper.Map<UsuarioDto>(usuarioAlterado);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ServiceException("Ocorreu um erro ao tentar alterar o usuário.", ex);
            }
        }

        public async Task<UsuarioDto> Excluir(int id)
        {
            try
            {
                var usuarioExcluido = await _repository.Excluir(id);
                if (usuarioExcluido == null)
                {
                    throw new NotFoundException($"Usuário não encontrado.");
                }
                return _mapper.Map<UsuarioDto>(usuarioExcluido);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ServiceException("Ocorreu um erro ao tentar excluir o usuário.", ex);
            }
        }

        public async Task<UsuarioDto> Incluir(UsuarioDto usuarioDto)
        {
            try
            {
                var usuario = _mapper.Map<Usuarios>(usuarioDto);
                var usuarioIncluido = await _repository.Incluir(usuario);
                return _mapper.Map<UsuarioDto>(usuarioIncluido);
            }
            catch (Exception ex)
            {
                // Log the exception
                throw new ServiceException("Ocorreu um erro ao tentar incluir o usuário.", ex);
            }
        }

        public async Task<UsuarioDto> SelecionarAsync(int id)
        {
            try
            {
                var usuario = await _repository.SelecionarAsync(id);
                if (usuario == null)
                {
                    throw new NotFoundException($"Usuário não encontrado.");
                }
                return _mapper.Map<UsuarioDto>(usuario);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Ocorreu um erro ao tentar buscar o usuário.", ex);
            }
        }

        public async Task<IEnumerable<UsuarioDto>> SelecionarTodosAsync()
        {
            try
            {
                var usuarios = await _repository.SelecionarTodosAsync();
                return _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Ocorreu um erro ao tentar buscar os usuários.", ex);
            }
        }
    }
}
