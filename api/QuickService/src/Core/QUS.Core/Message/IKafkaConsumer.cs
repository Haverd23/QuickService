using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Core.Message
{
    public interface IKafkaConsumer
    {
        Task SubscribeAsync(string topic, Func<string, string, Task> messageHandler, CancellationToken cancellationToken = default);

    }
}
