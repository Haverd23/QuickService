using Moq;
using QUS.Users.Application.Commands;
using QUS.Users.Application.CommandsHandlers;
using QUS.Users.Domain.Interfaces;
using QUS.Users.Domain.Models;

namespace UserTests.Application
{
    public class UserApplication
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        public UserApplication()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact(DisplayName = "Deve criar um usuário e retornar o ID")]
        public async Task HandleAsync_DeveCriarUsuarioERetornarId()
        {
            // Arrange
            var name = "João";
            var email = "joao123@gmail.com";
            var phone = "11999999999";

            var command = new CreateUserCommand(name, email, phone);
            var commandHandler = new CreateUserCommandHandler(_userRepositoryMock.Object);

            _userRepositoryMock.Setup(x => x.UnitOfWork.Commit()).
                ReturnsAsync(true);

            // Act
            var result = await commandHandler.HandleAsync(command);

            // Assert
            _userRepositoryMock.Verify(x => x.Add(It.IsAny<User>()), Times.Once);
            _userRepositoryMock.Verify(x => x.UnitOfWork.Commit(), Times.Once);


        }
    }
}