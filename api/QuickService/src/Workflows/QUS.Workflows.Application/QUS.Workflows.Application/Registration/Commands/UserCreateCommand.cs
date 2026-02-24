using QUS.Core.Mediator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QUS.Workflows.Application.Registration.Commands
{
    public class UserCreateCommand : ICommand<Guid>
    {
        public string Name { get; }
        public string Email { get; }
        public string Phone { get; }
        public Guid AuthId { get; }

        public UserCreateCommand(string name, string email, string phone, Guid authId)
        {
            Name = name;
            Email = email;
            Phone = phone;
            AuthId = authId;
        }
    }
}
