using QUS.Core.Mediator.Commands;

namespace QUS.Auth.Application.Commands
{
    public class UserCreateCommand : ICommand<Guid>
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public UserCreateCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
