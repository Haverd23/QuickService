using QUS.Auth.Application.Kafka.Events;
using QUS.Auth.Domain.Interfaces;
using QUS.Core.IntegrationEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Auth.Application.Kafka.EventsHandlers
{
    public class UserCreateFailedEventHandler : IIntegrationEventHandler<UserCreateFailedEvent>
    {
        private readonly IAuthRepository _authRepository;

        public UserCreateFailedEventHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task HandleAsync(UserCreateFailedEvent @evento)
        {
            var userId = evento.AuthId;
            await _authRepository.DeleteAsync(userId);
                var success = await _authRepository.UnitOfWork.Commit();
    
                if (!success)
                {
                    Console.WriteLine($"🚨 Falha ao deletar usuário com AuthId: {userId}");
            }
        }
    }
}
