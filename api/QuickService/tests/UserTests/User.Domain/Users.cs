using QUS.Users.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserTests.Domain
{
    public class Users
    {
        [Fact(DisplayName = "Criar User com dados válidos")]
        public void Construtor_QuandoDadosValidos_DeveInstanciar()
        {
            // Arrange
            var name = "João Silva";
            var email = "joao@gmail.com";
            var phone = "11987654321";

            // Act
            var user = new User(name, email, phone, Guid.NewGuid());

            // Assert
            Assert.NotNull(user);
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
            Assert.Equal(phone, user.Phone.Number);

        }
        [Fact(DisplayName = "Criar User com email inválido lança exceção")]
        public void Construtor_QuandoEmailInvalido_DeveLancarExcecao()
        {
            // Arrange
            var name = "João Silva";
            var email = "joaogmail.com"; // Email inválido
            var phone = "11987654321";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new User(name, email, phone, Guid.NewGuid()));
            Assert.Equal("Email inválido", exception.Message);
        }
        [Fact(DisplayName = "Criar User com telefone inválido lança exceção")]
        public void Construtor_QuandoTelefoneInvalido_DeveLancarExcecao()
        {
            // Arrange
            var name = "João Silva";
            var email = "joao@gmail.com";
            var phone = "123"; // Telefone inválido

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new User(name, email, phone, Guid.NewGuid()));
            Assert.Equal("Telefone inválido", exception.Message);

        }
        [Fact(DisplayName = "Criar User com nome vazio lança exceção")]
        public void Construtor_QuandoNomeVazio_DeveLancarExcecao()
        {
            // Arrange
            var name = ""; // Nome vazio
            var email = "joao@gmail.com";
            var phone = "11987654321";

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new User(name, email, phone, Guid.NewGuid()));
            Assert.Equal("Nome não pode ser vazio", exception.Message);
        }
    }
}
