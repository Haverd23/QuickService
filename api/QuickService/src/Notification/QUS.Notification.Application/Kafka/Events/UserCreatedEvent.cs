using QUS.Core.IntegrationEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QUS.Notification.Application.Kafka.Events
{
    public class UserCreatedEvent : ApplicationEvent
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public UserCreatedEvent(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}
