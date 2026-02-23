using QUS.Auth.Application.Interfaces;
using QUS.Auth.Application.Kafka.Events;
using QUS.Auth.Domain.Interfaces;
using QUS.Auth.Domain.Models;
using QUS.Core.IntegrationEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Application.Kafka.EventsHandlers
{
    public class AuthCreateEventHandler: IIntegrationEventHandler<AuthCreateEvent>

    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordEncryption _passwordEncryption;

        public AuthCreateEventHandler(IAuthRepository authRepository, IPasswordEncryption passwordEncryption)
        {
            _authRepository = authRepository;
            _passwordEncryption = passwordEncryption;
        }

        public  async Task HandleAsync(AuthCreateEvent @evento)
        {
            var emailExists = await _authRepository.GetByEmailAsync(evento.Email);
            if (emailExists != null)
            {
                throw new Exception("Email already exists");
            }
            var senhaHash = _passwordEncryption.PasswordHash(evento.Password);
            var user = new User(evento.EventId,evento.Email, senhaHash);
            await _authRepository.AddAsync(user);
            var success = await _authRepository.UnitOfWork.Commit();
            if (!success)
            {
                throw new Exception("Error saving user");
            }
        }
    }
}
