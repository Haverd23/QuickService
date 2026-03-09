using QUS.Core.DomainObjects;
using QUS.Core.Exceptions;
using QUS.Users.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace QUS.Users.Domain.Models
{
    public class User : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Phone Phone { get; private set; }
        public Guid AuthId { get; private set; }

        protected User() { }
        public User(string name, string email, string phone, Guid authId)
        {
            IsValid(name, email);
            Name = name;
            Email = email;
            Phone = new Phone(phone);
            AuthId = authId;
        }
        private void IsValid(string name, string email)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new AppException("Nome não pode ser vazio",400);
            }
            if(name.Length < 3)
            {
                throw new AppException("Nome deve ter pelo menos 3 caracteres", 400);
            }
            if(!email.Contains("@"))
            {
                throw new AppException("Email inválido",400);
            }
          

        }
    }
}
