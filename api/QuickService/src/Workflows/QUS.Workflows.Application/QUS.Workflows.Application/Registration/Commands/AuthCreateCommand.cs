using QUS.Core.Mediator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QUS.Workflows.Application.Registration.Commands
{
    public class AuthCreateCommand : ICommand<Guid>
    {
        public Guid AuthId { get; }
        public string Email { get;  }
        public string Password { get;  }
        public AuthCreateCommand(Guid authId, string email, string password)
        {
            Email = email;
            Password = password;
            AuthId = authId;
        }
    }
}
