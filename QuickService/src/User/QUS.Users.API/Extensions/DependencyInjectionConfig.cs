using Microsoft.EntityFrameworkCore;
using QUS.Core.Mediator.Commands;
using QUS.Users.Application.Commands;
using QUS.Users.Application.CommandsHandlers;
using QUS.Users.Data;
using QUS.Users.Data.Repository;
using QUS.Users.Domain.Interfaces;

namespace QUS.Users.API.Extensions
{
    public static class DependencyInjectionConfig 
    {
        public static IServiceCollection AddDependencyInjection(this
            IServiceCollection services, IConfiguration configuration)
        {
            // Data
          services.AddScoped<IUserRepository, UserRepository>();
          services.AddDbContext<UserDbContext>(options => 
          options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Application
          services.AddScoped<ICommandHandler<CreateUserCommand, Guid>, CreateUserCommandHandler>();

           return services;
        }
    }
}
