using Amazon.Lambda.Model;
using AutoMapper;
using Ecommerce.Application.DTOs;
using Ecommerce.Application.Interfaces;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Pagination;
using OpenQA.Selenium;
using System.Security.Cryptography;
using System.Text;

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
                if (!string.IsNullOrEmpty(usuarioDto.Usu_ImgPerfilBase64))
                {
                    usuarioDto.Usu_ImgPerfil = Convert.FromBase64String(usuarioDto.Usu_ImgPerfilBase64);
                }

                var alterarUsuario = await _repository.SelecionarAsync((int)usuarioDto.Usu_id);

                if (alterarUsuario == null)
                {
                    throw new ServiceException("Usuário não encontrado.");
                }

                alterarUsuario.SetNome(usuarioDto.Usu_nome);
                alterarUsuario.SetEmail(usuarioDto.Usu_email);
                if (usuarioDto.Usu_ImgPerfil != null)
                {
                    alterarUsuario.SetImgPerfil(usuarioDto.Usu_ImgPerfil);
                }
                alterarUsuario.SetAdmin(usuarioDto.Usu_IsAdmin);

                var usuarioAlterado = await _repository.Alterar(alterarUsuario);

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

                if (usuarioDto.Usu_senha != null)
                {
                    using var hmac = new HMACSHA512();
                    byte[] _usu_senhaHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(usuarioDto.Usu_senha));
                    byte[] _usu_senhaSalt = hmac.Key;

                    usuario.AlterarSenha(_usu_senhaHash, _usu_senhaSalt);
                }

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
                return _mapper.Map<UsuarioDto>(usuario);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Ocorreu um erro ao tentar buscar o usuário.", ex);
            }
        }

        public async Task<UsuarioDto> SelecionarPorEmailAsync(string email)
        {
            try
            {
                var usuario = await _repository.SelecionarPorEmailAsync(email);
                return _mapper.Map<UsuarioDto>(usuario);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Ocorreu um erro ao tentar buscar o usuário.", ex);
            }
        }

        public async Task<PagedList<UsuarioDto>> SelecionarTodosAsync(int pageNumber, int pageSize)
        {
            try
            {
                var usuarios = await _repository.SelecionarTodosAsync(pageNumber, pageSize);
                var usuariosDto = _mapper.Map<IEnumerable<UsuarioDto>>(usuarios);
                return new PagedList<UsuarioDto>(usuariosDto, pageNumber, pageSize, usuarios.TotalCount);
            }
            catch (Exception ex)
            {
                throw new ServiceException("Ocorreu um erro ao tentar buscar os usuários.", ex);
            }
        }
    }
}
