using Confluent.Kafka;
using QUS.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Users.Data.Messaging.Kafka
{
    public class KafkaConsumer : IKafkaConsumer, IDisposable
    {
        private readonly IConsumer<string, string> _consumer;

        public KafkaConsumer(ConsumerConfig config)
        {
            _consumer = new ConsumerBuilder<string, string>(config).Build();
        }

        public async Task SubscribeAsync(
    string topic,
    Func<string, string, Task> messageHandler,
    CancellationToken cancellationToken = default)
        {
            _consumer.Subscribe(topic);

            while (!cancellationToken.IsCancellationRequested)
            {
                ConsumeResult<string, string>? result = null;

                try
                {
                    result = _consumer.Consume(cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ConsumeException)
                {
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao processar mensagem: {ex.Message}");
                }

                if (result?.Message == null)
                    continue;

                try
                {
                    await messageHandler(
                        result.Message.Key,
                        result.Message.Value);

                    _consumer.Commit(result);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"🔥 Erro dentro do handler: {ex.Message}");

                }
            }
        }

        public void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
        }
    }
}