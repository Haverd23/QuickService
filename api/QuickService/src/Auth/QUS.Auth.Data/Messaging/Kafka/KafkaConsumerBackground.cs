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

        private const string Topic = "AuthCreateEvent";

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

            await _consumer.SubscribeAsync(
                Topic,
                async (key, value) =>
                {
                    Console.WriteLine($"📩 Mensagem recebida: {value}");

                    using var scope = _serviceProvider.CreateScope();

                    await _dispatcher.DispatchAsync(
                        Topic,
                        value,
                        scope);
                },
                stoppingToken);
        }
    }
}