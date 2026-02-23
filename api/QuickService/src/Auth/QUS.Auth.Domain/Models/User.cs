using QUS.Auth.Domain.DomainEvents;
using QUS.Auth.Domain.ValueObjects;
using QUS.Core.DomainObjects;

namespace QUS.Auth.Domain.Models
{
    public class User : Entity, IAggregateRoot
    {
        protected User() { }
        public Email Email { get; private set; }
        public string Password { get; private set; }

        public User(Guid authId,string email, string password)
        {
            Email = new Email(email);
            PasswordValidade(password);
            Password = password;
            AddDomainEvent(new UserCreateEvent(Id, Email.Entrada));
        }

        public static void PasswordValidade(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Senha inválida");
            if (password.Length < 6)
                throw new Exception("Senha deve ter pelo menos 6 caracteres");
            if (!password.Any(char.IsDigit))
                throw new Exception("Senha deve conter pelo menos um número");
            if (!password.Any(char.IsUpper))
                throw new Exception("Senha deve conter pelo menos uma letra maiúscula");
            if (!password.Any(char.IsLower))
                throw new Exception("Senha deve conter pelo menos uma letra minúscula");
        }
    }
}
