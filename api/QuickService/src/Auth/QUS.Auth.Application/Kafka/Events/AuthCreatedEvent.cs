using QUS.Core.IntegrationEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Application.Kafka.Events
{
    public class AuthCreatedEvent : ApplicationEvent
    {
        public Guid AuthId { get; }
        public string Email { get; }
        public string Name { get; }
        public string Phone { get; }

        public AuthCreatedEvent(Guid authId, string email, string name, string phone)
        {
            AuthId = authId;
            Email = email;
            Name = name;
            Phone = phone;
        }
    }
}
