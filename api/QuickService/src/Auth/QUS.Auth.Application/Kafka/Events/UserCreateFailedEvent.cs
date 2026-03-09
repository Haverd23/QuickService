using QUS.Core.IntegrationEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Application.Kafka.Events
{
    public class UserCreateFailedEvent : ApplicationEvent
    {
        public Guid AuthId { get; set; }
        public UserCreateFailedEvent(Guid authId)
        {
            AuthId = authId;
        }
    }
}
