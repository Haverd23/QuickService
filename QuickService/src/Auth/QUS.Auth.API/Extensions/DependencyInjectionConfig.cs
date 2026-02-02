using Microsoft.EntityFrameworkCore;
using QUS.Auth.Application.Commands;
using QUS.Auth.Application.CommandsHandlers;
using QUS.Auth.Application.Interfaces;
using QUS.Auth.Application.Services;
using QUS.Auth.Data;
using QUS.Auth.Data.Repository;
using QUS.Auth.Domain.Interfaces;
using QUS.Core.Mediator.Commands;

namespace QUS.Auth.API.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this
            IServiceCollection services, IConfiguration configuration)
        {
            // Data
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddDbContext<AuthDbContext>((options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))));

            // Application
            services.AddScoped<ICommandHandler<UserCreateCommand, Guid>,
                UserCreateCommandHandler>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
            services.AddScoped<ITokenJWT, TokenJWT>();
            services.AddScoped<IPasswordEncryption, PasswordEncryption>();
            services.AddScoped<ILogin, LoginService>();
            return services;


        }
    }
}
