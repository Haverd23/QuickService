using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QUS.Auth.Application.Kafka.Events;
using QUS.Auth.Application.Kafka.EventsHandlers;
using QUS.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QUS.Auth.Data.Messaging.Kafka
{
    public class KafkaConsumerBackground : BackgroundService
    {
        private readonly IKafkaConsumer _consumer;
        private readonly IServiceProvider _serviceProvider;
        private readonly GenericKafkaDispatcher _dispatcher;

        private const string Topic = "RegistrationEvent";
        private const string Topic2 = "UserCreateFailedEvent";      

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
                new[] { Topic, Topic2 },
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