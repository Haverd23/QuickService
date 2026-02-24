using QUS.Workflows.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Workflows.Application.Registration.Events
{
    public class UserCreateEvent : ApplicationEvent
    {
        public string Name { get; }
        public string Email { get; }
        public string Phone { get; }
        public Guid AuthId { get; }

        public UserCreateEvent(string name, string email, string phone, Guid authId)
        {
            Name = name;
            Email = email;
            Phone = phone;
            AuthId = authId;
        }
    }
}
