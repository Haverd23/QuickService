using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QUS.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Users.Data.Messaging.Kafka
{
    public class KafkaConsumerBackground : BackgroundService
    {
        private readonly IKafkaConsumer _consumer;
        private readonly IServiceProvider _serviceProvider;
        private readonly GenericKafkaDispatcher _dispatcher;

        private const string Topic = "UserCreateEvent";

        public KafkaConsumerBackground(
            IKafkaConsumer consumer,
            IServiceProvider serviceProvider,
            GenericKafkaDispatcher dispatcher)
        {
            _consumer = consumer;
            _serviceProvider = serviceProvider;
            _dispatcher = dispatcher;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("🔥 KafkaConsumerBackground iniciado");

            Task.Run(async () =>
            {
                await _consumer.SubscribeAsync(
                    Topic,
                    async (key, value) =>
                    {
                        Console.WriteLine($"📩 Mensagem recebida: {value}");
                        using var scope = _serviceProvider.CreateScope();
                        await _dispatcher.DispatchAsync(Topic, value, scope);
                    },
                    stoppingToken);
            }, stoppingToken);

            return Task.CompletedTask; 
        }
    }
}