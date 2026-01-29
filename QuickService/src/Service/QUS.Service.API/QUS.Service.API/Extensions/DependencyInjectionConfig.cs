using Microsoft.EntityFrameworkCore;
using QUS.Core.Mediator.Commands;
using QUS.Service.Application.Commands;
using QUS.Service.Application.Service;
using QUS.Services.Application.CommandsHandlers;
using QUS.Services.Data;
using QUS.Services.Data.Repository;
using QUS.Services.Domain.Interfaces;

namespace QUS.Service.API.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this
            IServiceCollection services, IConfiguration configuration)
        {
            // Data
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddDbContext<ServiceDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Application
            services.AddScoped<ICommandHandler<CreateServiceCommand, Guid>, CreateServiceCommandHandler>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();

            return services;
        }
    }
}
