using QUS.Core.Mediator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QUS.Workflows.Application.Registration.Commands
{
    public class RegisterAuthCommand : ICommand<Guid>
    {
        public string Email { get; }
        public string Password { get; }
        public Guid Id { get;}
        public RegisterAuthCommand(string email, string password, Guid id)
        {
            Email = email;
            Password = password;
            Id = id;
        }
    }
}
