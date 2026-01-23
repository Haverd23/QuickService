using QUS.Core.Mediator.Commands;
using QUS.Users.Application.Commands;
using QUS.Users.Domain.Interfaces;
using QUS.Users.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Users.Application.CommandsHandlers
{
    public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Guid> HandleAsync(CreateUserCommand command)
        {
            var user = new User(command.Name, command.Email, command.Phone);
            await _userRepository.Add(user);
            var result = await _userRepository.UnitOfWork.Commit();
            if(!result)
            {
                throw new Exception("Error creating user");
            }
            return user.Id;
        }
    }
}
