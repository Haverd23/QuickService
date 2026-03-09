using QUS.Auth.Application.Interfaces;
using QUS.Auth.Application.Kafka.Events;
using QUS.Auth.Domain.Interfaces;
using QUS.Auth.Domain.Models;
using QUS.Core.Exceptions;
using QUS.Core.IntegrationEvent;
using QUS.Core.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QUS.Auth.Application.Kafka.EventsHandlers
{
    public class AuthCreateEventHandler: IIntegrationEventHandler<RegistrationEvent>

    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordEncryption _passwordEncryption;
        private readonly IEventBus _bus;

        public AuthCreateEventHandler(IAuthRepository authRepository,
            IPasswordEncryption passwordEncryption, IEventBus bus)
        {
            _authRepository = authRepository;
            _passwordEncryption = passwordEncryption;
            _bus = bus;
        }

        public  async Task HandleAsync(RegistrationEvent @evento)
        {
            var emailExists = await _authRepository.GetByEmailAsync(evento.Email);
            if (emailExists != null)
            {
                throw new AppException("Email already exists",409);
            }
            var senhaHash = _passwordEncryption.PasswordHash(evento.Password);
            var user = new User(evento.AuthId,evento.Email, senhaHash);
            await _authRepository.AddAsync(user);
            var success = await _authRepository.UnitOfWork.Commit();
            if (!success)
            {
                throw new AppException("Error saving user",500);
            }
            var authCreatedEvent = new AuthCreatedEvent(evento.AuthId,evento.Email,evento.Name,evento.Phone);
            var topic = authCreatedEvent.EventType;

            var payload = JsonSerializer.Serialize(authCreatedEvent);

            await _bus.PublishAsync(
                topic,
                authCreatedEvent.EventId.ToString(),
                payload
            );

         

        }
    }
}
