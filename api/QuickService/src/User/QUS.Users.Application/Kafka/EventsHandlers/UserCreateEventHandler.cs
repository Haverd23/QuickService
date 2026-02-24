using QUS.Core.IntegrationEvent;
using QUS.Users.Application.Kafka.Events;
using QUS.Users.Domain.Interfaces;
using QUS.Users.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Users.Application.Kafka.EventsHandlers
{
    public class UserCreateEventHandler : IIntegrationEventHandler<UserCreateEvent>
    {
        private readonly IUserRepository _userRepository;
        public UserCreateEventHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task HandleAsync(UserCreateEvent evento)
        {
            var user = new User(evento.Name, evento.Email, evento.Phone, evento.AuthId);
            await _userRepository.Add(user);
            var success = await _userRepository.UnitOfWork.Commit();
            if (!success)
            {
                throw new Exception("Error saving user");
            }


        }
    }
}
