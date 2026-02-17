using QUS.Core.Mediator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QUS.Workflows.Application.Registration
{
    public sealed record RegisterUserCommand : ICommand<Guid>
    {
        public string Email { get; }
        public string Password { get; }
        public string Name { get; }
        public string Phone { get; }

        public RegisterUserCommand(string email, string password, string name, string phone)
        {
            Email = email;
            Password = password;
            Name = name;
            Phone = phone;

        }
    }
}
