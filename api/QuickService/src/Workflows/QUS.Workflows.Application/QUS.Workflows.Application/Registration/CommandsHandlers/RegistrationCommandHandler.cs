using QUS.Core.Mediator.Commands;
using QUS.Core.Message;
using QUS.Workflows.Application.Registration.Commands;
using QUS.Workflows.Application.Registration.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QUS.Workflows.Application.Registration.CommandsHandlers
{
    public class RegistrationCommandHandler : ICommandHandler<RegistrationCommand, Guid>
    {
        private readonly IEventBus _eventBus;
        public RegistrationCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public async Task<Guid> HandleAsync(RegistrationCommand command)
        {
            var applicationEvent = new RegistrationEvent(
                command.AuthId,
                command.Email,
                command.Password,
                command.Name,
                command.Phone

            );

            var topic = applicationEvent.EventType;

            var payload = JsonSerializer.Serialize(applicationEvent);

            await _eventBus.PublishAsync(
                topic,
                applicationEvent.EventId.ToString(),
                payload
            );

            return applicationEvent.AuthId;
        }
    }
}

