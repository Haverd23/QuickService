namespace QUS.Users.Domain.Models
{
    public class User
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }

        public User(string name, string email, string phone)
        {
            IsValid(name, email, phone);
            Name = name;
            Email = email;
            Phone = phone;
        }
        private void IsValid(string name, string email, string phone)
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
            if(phone.Length < 10)
            {
                throw new ArgumentException("Telefone inválido");
            }

        }
    }
}
