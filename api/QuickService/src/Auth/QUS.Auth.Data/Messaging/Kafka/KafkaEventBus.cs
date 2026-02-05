using QUS.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Data.Messaging.Kafka
{
    public class KafkaEventBus : IEventBus
    {
        private readonly IKafkaProducer _producer;

        public KafkaEventBus(IKafkaProducer producer)
        {
            _producer = producer;
        }

        public async Task PublishAsync(string topic, string key, string payload)
            => await _producer.PublishAsync(topic, key, payload);
    }
}