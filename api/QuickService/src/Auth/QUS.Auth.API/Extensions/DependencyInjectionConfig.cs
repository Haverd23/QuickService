using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using QUS.Auth.Application.Commands;
using QUS.Auth.Application.CommandsHandlers;
using QUS.Auth.Application.Interfaces;
using QUS.Auth.Application.Services;
using QUS.Auth.Data;
using QUS.Auth.Data.Messaging;
using QUS.Auth.Data.Messaging.Kafka;
using QUS.Auth.Data.Repository;
using QUS.Auth.Domain.Interfaces;
using QUS.Core.DomainObjects;
using QUS.Core.Mediator.Commands;
using QUS.Core.Message;

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

            // Events
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();

            // Kafka
            services.AddSingleton<IKafkaProducer, KafkaProducer>();
            services.AddSingleton<IEventBus, KafkaEventBus>();
            services.AddSingleton(sp =>
            {
                var kafkaSection = configuration.GetSection("Kafka");
                var producerSection = kafkaSection.GetSection("Producer");

                return new ProducerConfig
                {
                    BootstrapServers = kafkaSection["BootstrapServers"],
                    Acks = Acks.All,
                    EnableIdempotence = producerSection.GetValue<bool>("EnableIdempotence"),
                    LingerMs = producerSection.GetValue<int>("LingerMs"),
                    MessageTimeoutMs = producerSection.GetValue<int>("MessageTimeoutMs")
                };
            });

            return services;





        }
    }
}
