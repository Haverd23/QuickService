using QUS.Core.IntegrationEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Workflows.Application.Registration.Events
{
    public class RegistrationEvent : ApplicationEvent
    {
        public Guid AuthId { get; }
        public string Email { get; }
        public string Password { get;}
        public string Name { get; }
        public string Phone { get; }

        public RegistrationEvent(Guid authId,string email, string password, string name,
            string phone)
        {
            AuthId = authId;    
            Email = email;
            Password = password;
            Name = name;
            Phone = phone;
        }
    }
}
