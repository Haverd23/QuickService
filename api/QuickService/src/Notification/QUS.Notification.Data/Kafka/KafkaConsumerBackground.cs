using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QUS.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Notification.Data.Kafka
{
    public class KafkaConsumerBackground : BackgroundService
    {
        private readonly IKafkaConsumer _consumer;
        private readonly IServiceProvider _serviceProvider;
        private readonly GenericKafkaDispatcher _dispatcher;

        private const string Topic = "UserCreatedEvent";

        public KafkaConsumerBackground(
            IKafkaConsumer consumer,
            IServiceProvider serviceProvider,
            GenericKafkaDispatcher dispatcher)
        {
            _consumer = consumer;
            _serviceProvider = serviceProvider;
            _dispatcher = dispatcher;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("🔥 KafkaConsumerBackground iniciado");

            var task1 = Task.Run(async () =>
            {
                await _consumer.SubscribeAsync(
                new[] { Topic },
                async (topic, value) =>
                {
                    using var scope = _serviceProvider.CreateScope();
                    await _dispatcher.DispatchAsync(topic, value, scope);
                },
                stoppingToken);
            }, stoppingToken);


            await Task.WhenAll(task1);
        }
    }
}