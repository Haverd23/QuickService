using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Core.Message
{
    public interface IEventBus
    {
        Task PublishAsync(string topic, string key, string payload);

    }
}
