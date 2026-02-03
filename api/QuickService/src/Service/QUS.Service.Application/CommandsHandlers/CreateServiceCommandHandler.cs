using QUS.Core.Mediator.Commands;
using QUS.Service.Application.Commands;
using QUS.Services.Domain.Enums;
using QUS.Services.Domain.Interfaces;
using DomainService = QUS.Services.Domain.Models.Service;

namespace QUS.Services.Application.CommandsHandlers
{
    public class CreateServiceCommandHandler : ICommandHandler
        <CreateServiceCommand, Guid>
    {
        private readonly IServiceRepository _serviceRepository;
        public CreateServiceCommandHandler(
            IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }
        public async Task<Guid> HandleAsync(CreateServiceCommand command)
        {
            if (!Enum.TryParse<Category>(command.Category, true, out var category))
            {
                throw new Exception("Categoria inválida");
            }

            var service = new DomainService(

                command.Title,
                command.Description,
                command.Price,
                command.ProviderId,
                category
            );
            await _serviceRepository.AddAsync(service);
            var success = await _serviceRepository.UnitOfWork.Commit();
            if (!success)
            {
                throw new Exception("Ocorreu um erro ao criar o serviço");
            }
            return service.Id;

        }
    }
}
