using QUS.Core.IntegrationEvent;
using QUS.Core.Message;
using QUS.Users.Application.Kafka.Events;
using QUS.Users.Domain.Interfaces;
using QUS.Users.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QUS.Users.Application.Kafka.EventsHandlers
{
    public class UserCreateEventHandler : IIntegrationEventHandler<AuthCreatedEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventBus _eventBus;
        public UserCreateEventHandler(IUserRepository userRepository,
            IEventBus eventBus)
        {
            _userRepository = userRepository;
            _eventBus = eventBus;
        }

        public async Task HandleAsync(AuthCreatedEvent evento)
        {
            try
            {
                var userExists = await _userRepository.GetByEmail(evento.Email);

                if (userExists != null)
                {
                    await PublishFailure(evento.AuthId);
                    return;
                }

                var user = new User(
                    evento.Name,
                    evento.Email,
                    evento.Phone,
                    evento.AuthId
                );

                await _userRepository.Add(user);

                var success = await _userRepository.UnitOfWork.Commit();

                if (!success)
                {
                    await PublishFailure(evento.AuthId);
                }
                await PublishSuccess(evento.Email, evento.Name);

            }
            catch
            {
                await PublishFailure(evento.AuthId);
            }
        }

        private async Task PublishFailure(Guid authId)
        {
            var userFailed = new UserCreateFailedEvent(authId);

            var topic = userFailed.EventType;

            var payload = JsonSerializer.Serialize(userFailed);

            await _eventBus.PublishAsync(
                topic,
                userFailed.EventId.ToString(),
                payload
            );
        }
        private async Task PublishSuccess(string email, string name)
        {
            var userCreated = new UserCreatedEvent(email,name);
            var topic = userCreated.EventType;
            var payload = JsonSerializer.Serialize(userCreated);
            await _eventBus.PublishAsync(
                topic,
                userCreated.EventId.ToString(),
                payload
            );
        }
    }
}