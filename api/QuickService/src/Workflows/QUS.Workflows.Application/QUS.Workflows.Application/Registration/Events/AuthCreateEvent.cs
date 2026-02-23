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
        public string Email { get; }
        public string Password { get;}

        public AuthCreateEvent(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
