using QUS.Auth.Domain.Models;

namespace Auth.Tests.Domain
{
    public class AuthTests
    {
        [Fact(DisplayName = "Criar User com dados válidos")]
        public void Construtor_QuandoDadosValidos_DeveInstanciar()
        {
            // Arrange
            var email = "teste@gmail.com";
            var password = "Senha123";

            // Act
            var user = new User(Guid.NewGuid(),email, password);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(email, user.Email.Entrada);
            Assert.Equal(password, user.Password);


        }
        [Fact(DisplayName = "Criar User com senha inválida lança Exceção")]
        public void Construtor_QuandoSenhaInvalida_DeveLancarExcecao()
        {
            // Arrange
            var email = "teste@gmail.com";
            var password = "123"; // Senha inválida

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => new User(Guid.NewGuid(), email, password));
        }
        [Fact(DisplayName = "Criar User com email inválido lança Exceção")]
        public void Construtor_QuandoEmailInvalido_DeveLancarExcecao()
        {
            // Arrange
            var email = "teste.com"; // Email inválido
            var password = "Senha123";
            // Act & Assert
            var exception = Assert.Throws<Exception>(() => new User(Guid.NewGuid(), email, password));
        }
        [Fact(DisplayName = "Criar User com senha sem número lança Exceção")]
        public void Construtor_QuandoSenhaSemNumero_DeveLancarExcecao()
        {
            // Arrange
            var email = "teste@gmail.com";
            var password = "Senha"; // Senha sem número

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => new User(Guid.NewGuid(), email, password));


        }
        [Fact(DisplayName = "Criar User com senha sem letra maiúscula lança Exceção")]
        public void Construtor_QuandoSenhaSemLetraMaiuscula_DeveLancarExcecao()
        {
            // Arrange
            var email = "teste@gmail.com";
            var password = "senha123"; // Senha sem letra maiúscula

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => new User(Guid.NewGuid(), email, password));

        }
    }
}
