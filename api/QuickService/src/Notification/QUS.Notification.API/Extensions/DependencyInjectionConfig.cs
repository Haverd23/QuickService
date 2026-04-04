using Confluent.Kafka;
using QUS.Core.IntegrationEvent;
using QUS.Core.Message;
using QUS.Notification.Application.Interfaces;
using QUS.Notification.Application.Kafka.Events;
using QUS.Notification.Application.Kafka.EventsHandlers;
using QUS.Notification.Application.Services;
using QUS.Notification.Data.Kafka;
using QUS.Notification.Data.Services;
using QUS.Notification.Data.Settings;

namespace QUS.Notification.API.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfig(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<EmailSettings>(
                configuration.GetSection("EmailSettings"));

            services.AddScoped<IEmailSender, MailKitEmailSender>();
            services.AddScoped<NotificationService>();
            services.AddSingleton<IKafkaConsumer, KafkaConsumer>();
            services.AddScoped<IIntegrationEventHandler<UserCreatedEvent>, UserCreatedEventHandler>();

            services.AddSingleton<GenericKafkaDispatcher>();
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();

                return new ConsumerConfig
                {
                    BootstrapServers = configuration["Kafka:BootstrapServers"],
                    GroupId = "notification-consumer-group",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = false
                };
            });
            services.AddHostedService<KafkaConsumerBackground>();

            return services;
        }
    }
}