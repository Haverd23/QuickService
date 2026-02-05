using QUS.Core.DomainObjects;
using QUS.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QUS.Auth.Data.Messaging
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IEventBus _eventBus;

        public DomainEventDispatcher(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task DispatchEventsAsync(IEnumerable<IDomainEvent> events)
        {
            foreach (var evt in events)
            {
                var topic = evt.GetType().Name;
                var payload = JsonSerializer.Serialize(evt);
                var aggregateId = evt.AggregateId;

                await _eventBus.PublishAsync(
                    topic,
                    aggregateId.ToString(),
                    payload
                );
            }
        }
    }
}
