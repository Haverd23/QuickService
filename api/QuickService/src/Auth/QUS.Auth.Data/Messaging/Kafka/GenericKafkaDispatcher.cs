using Microsoft.Extensions.DependencyInjection;
using QUS.Core.IntegrationEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QUS.Auth.Data.Messaging.Kafka
{
    public class GenericKafkaDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Dictionary<string, Type> _eventTypes;

        public GenericKafkaDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            _eventTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.Name.EndsWith("Event"))
            .ToDictionary(t => t.Name, t => t);
        }


        public async Task DispatchAsync(
            string topic,
            string message,
            IServiceScope scope)
        {
            if (!_eventTypes.TryGetValue(topic, out var eventType))
                return;

            var @event = JsonSerializer.Deserialize(
                message,
                eventType,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (@event is null)
                return;

            var handlerType = typeof(IIntegrationEventHandler<>)
                .MakeGenericType(eventType);

            var handler = scope.ServiceProvider.GetService(handlerType);

            if (handler == null)
            {
                Console.WriteLine($"🚨 Handler não encontrado para {eventType.Name}");
                return;
            }
            var method = handlerType.GetMethod("HandleAsync");

            await (Task)method.Invoke(handler, new[] { @event });
        }
    }
}