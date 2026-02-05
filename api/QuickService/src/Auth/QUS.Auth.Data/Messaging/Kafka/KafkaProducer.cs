using Confluent.Kafka;
using QUS.Core.Message;
namespace QUS.Auth.Data.Messaging.Kafka
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<string, string> _producer;

        public KafkaProducer(ProducerConfig config)
        {
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task PublishAsync(string topic, string key, string message)
        {
            await _producer.ProduceAsync(
                topic,
                new Message<string, string>
                {
                    Key = key,
                    Value = message
                }
            );
        }
    }
}
