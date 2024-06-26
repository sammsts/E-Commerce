﻿using Ecommerce.API.Mappings;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Services;
using Ecommerce.Domain.Account;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Infra.Data.Context;
using Ecommerce.Infra.Data.Identity;
using Ecommerce.Infra.Data.Repositories;
using Ecommerce.Interfaces.Repositorios;
using Ecommerce.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Ecommerce.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EcommerceContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")).EnableSensitiveDataLogging()
                   .LogTo(Console.WriteLine, LogLevel.Information);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["jwt:issuer"],
                    ValidAudience = configuration["jwt:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:secretKey"])), ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAutoMapper(typeof(EntitiesToDtoMappingProfile));

            #region Services
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<ICarrinhoService, CarrinhoService>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<ITokenService, TokenService>();
            #endregion
            #region Repositories
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ICarrinhoRepository, CarrinhoRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            #endregion

            return services;
        }
    }
} 
