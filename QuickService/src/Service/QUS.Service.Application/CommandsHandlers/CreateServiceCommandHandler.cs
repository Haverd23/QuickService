using QUS.Core.Mediator.Commands;
using QUS.Service.Application.Commands;
using QUS.Service.Application.Interfaces;
using QUS.Services.Domain.Enums;
using QUS.Services.Domain.Interfaces;
using DomainService = QUS.Services.Domain.Models.Service;

namespace QUS.Services.Application.CommandsHandlers
{
    public class CreateServiceCommandHandler : ICommandHandler
        <CreateServiceCommand, Guid>
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IGetUserIdService _getUserIdService;
        public CreateServiceCommandHandler(
            IServiceRepository serviceRepository,
            IGetUserIdService getUserIdService)
        {
            _serviceRepository = serviceRepository;
            _getUserIdService = getUserIdService;
        }
        public async Task<Guid> HandleAsync(CreateServiceCommand command)
        {
           var commandUserId = await _getUserIdService.GetUserId(command.ProviderId);
            if (!Enum.TryParse<Category>(command.Category, true, out var category))
            {
                throw new Exception("Categoria inválida");
            }

            var service = new DomainService(

                command.Title,
                command.Description,
                command.Price,
                commandUserId,
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
