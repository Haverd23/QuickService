using QUS.Core.Mediator.Commands;
using QUS.Core.Message;
using QUS.Workflows.Application.Registration.Commands;
using QUS.Workflows.Application.Registration.Events;
using System.Text.Json;


namespace QUS.Workflows.Application.Registration.CommandsHandlers
{
    public class UserCreateCommandHandler : ICommandHandler<UserCreateCommand, Guid>
    {
        private readonly IEventBus _eventBus;
        public UserCreateCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }
        public async Task<Guid> HandleAsync(UserCreateCommand command)
        {
            var applicationEvent = new UserCreateEvent
                (command.Name, command.Email, command.Phone, command.AuthId);

            var topic = applicationEvent.EventType;

            var payload = JsonSerializer.Serialize(applicationEvent);

            await _eventBus.PublishAsync(
                topic,
                applicationEvent.EventId.ToString(),
                payload
            );

            return applicationEvent.EventId;
        }
    }
}
