using QUS.Core.IntegrationEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Users.Application.Kafka.Events
{
    public class AuthCreatedEvent : ApplicationEvent
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid AuthId { get; set; }

        public AuthCreatedEvent(string name, string email, string phone, Guid authId)
        {
            Name = name;
            Email = email;
            Phone = phone;
            AuthId = authId;
        }
    }
}
