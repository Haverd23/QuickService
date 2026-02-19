using QUS.Core.Mediator.Commands;
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
