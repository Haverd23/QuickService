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
    public class AuthCreateCommandHandler : ICommandHandler<AuthCreateCommand, Guid>
    {
        private readonly IEventBus _eventBus;
        public AuthCreateCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public async Task<Guid> HandleAsync(AuthCreateCommand command)
        {
            var applicationEvent = new AuthCreateEvent(
                command.AuthId,
                command.Email,
                command.Password
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

