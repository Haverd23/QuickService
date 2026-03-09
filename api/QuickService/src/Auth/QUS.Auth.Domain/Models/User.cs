using QUS.Auth.Domain.DomainEvents;
using QUS.Auth.Domain.ValueObjects;
using QUS.Core.DomainObjects;
using QUS.Core.Exceptions;

namespace QUS.Auth.Domain.Models
{
    public class User : Entity, IAggregateRoot
    {
        protected User() { }
        public Email Email { get; private set; }
        public string Password { get; private set; }

        public User(Guid authId,string email, string password): base(authId)
        {
            Email = new Email(email);
            PasswordValidade(password);
            Password = password;
            AddDomainEvent(new UserCreateEvent(Id, Email.Entrada));
        }

        public static void PasswordValidade(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new AppException("Senha inválida",400);
            if (password.Length < 6)
                throw new AppException("Senha deve ter pelo menos 6 caracteres", 400);
            if (!password.Any(char.IsDigit))
                throw new AppException("Senha deve conter pelo menos um número", 400);
            if (!password.Any(char.IsUpper))
                throw new AppException("Senha deve conter pelo menos uma letra maiúscula", 400);
            if (!password.Any(char.IsLower))
                throw new AppException("Senha deve conter pelo menos uma letra minúscula", 400);
        }
    }
}
