using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using QUS.Core.IntegrationEvent;
using QUS.Core.Mediator.Commands;
using QUS.Core.Message;
using QUS.Users.Application.Commands;
using QUS.Users.Application.CommandsHandlers;
using QUS.Users.Application.Kafka.Events;
using QUS.Users.Application.Kafka.EventsHandlers;
using QUS.Users.Application.Services;
using QUS.Users.Data;
using QUS.Users.Data.Messaging.Kafka;
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
          services.AddScoped<ICommandDispatcher, CommandDispatcher>();

            services.AddSingleton<IKafkaConsumer, KafkaConsumer>();
            services.AddSingleton<GenericKafkaDispatcher>();
            services.AddScoped<IIntegrationEventHandler<UserCreateEvent>, UserCreateEventHandler>();
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();

                return new ConsumerConfig
                {
                    BootstrapServers = configuration["Kafka:BootstrapServers"],
                    GroupId = "user-consumer-group",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = false
                };
            });
            services.AddHostedService<KafkaConsumerBackground>();


            return services;
        }
    }
}
