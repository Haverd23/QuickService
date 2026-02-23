using Confluent.Kafka;
using QUS.Core.Mediator.Commands;
using QUS.Core.Message;
using QUS.Workflows.Application.Registration.Commands;
using QUS.Workflows.Application.Registration.CommandsHandlers;
using QUS.Workflows.Application.Services;
using QUS.Workflows.Data.Kafka;

namespace QUS.Workflows.API.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjection(this
            IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICommandHandler<AuthCreateCommand, Guid>,
                AuthCreateCommandHandler>();
            services.AddScoped<ICommandDispatcher, CommandDispatcher>();
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
