using QUS.Workflows.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Workflows.Application.Registration.Events
{
    public class AuthCreateEvent : ApplicationEvent
    {
        public Guid AuthId { get; }
        public string Email { get; }
        public string Password { get;}

        public AuthCreateEvent(Guid authId,string email, string password)
        {
            AuthId = authId;    
            Email = email;
            Password = password;
        }
    }
}
