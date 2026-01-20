using QUS.Users.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace QUS.Users.Domain.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public Phone Phone { get; private set; }

        protected User() { }
        public User(string name, string email, string phone)
        {
            IsValid(name, email);
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Phone = new Phone(phone);
        }
        private void IsValid(string name, string email)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Nome não pode ser vazio");
            }
            if(name.Length < 3)
            {
                throw new ArgumentException("Nome deve ter pelo menos 3 caracteres");
            }
            if(!email.Contains("@"))
            {
                throw new ArgumentException("Email inválido");
            }
          

        }
    }
}
