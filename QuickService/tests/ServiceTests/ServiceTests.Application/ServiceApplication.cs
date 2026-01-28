using Moq;
using QUS.Core.Mediator.Commands;
using QUS.Service.Application.Commands;
using QUS.Service.Application.Interfaces;
using QUS.Services.Application.CommandsHandlers;
using QUS.Services.Domain.Interfaces;
using QUS.Services.Domain.Models;

namespace ServiceTests.Application
{
    public class ServiceApplication
    {
        private readonly Mock<IServiceRepository> _serviceRepositoryMock;
        private readonly Mock<IGetUserIdService> _getUserIdServiceMock;
        public ServiceApplication()
        {
            _serviceRepositoryMock = new Mock<IServiceRepository>();
            _getUserIdServiceMock = new Mock<IGetUserIdService>();
        }

        [Fact(DisplayName = "Deve criar um service e retornar um ID")]
        public async Task HandleAsync_DeveCriarServiceERetornarId()
        {
            // Arrange
            var title = "Service Teste";
            var description = "Descrição do service teste";
            var price = 150;
            var category = "Pedreiro";
            var providerId = Guid.NewGuid();
            var command = new CreateServiceCommand(title, description, price,providerId, category);
            var commandHandler = new CreateServiceCommandHandler(_serviceRepositoryMock.Object,
                _getUserIdServiceMock.Object);

            _serviceRepositoryMock.Setup(x => x.UnitOfWork.Commit()).
                ReturnsAsync(true);

            // Act
            var result = await commandHandler.HandleAsync(command);

            // Assert
            Assert.IsType<Guid>(result);
            _serviceRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Service>()), Times.Once);
            _serviceRepositoryMock.Verify(x => x.UnitOfWork.Commit(), Times.Once);

        }

    }
}