using QUS.Auth.Application.Commands;
using QUS.Core.Mediator.Commands;
using QUS.Users.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Workflows.Application.Registration
{
    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand, Guid>
    {
        private readonly ICommandDispatcher _dispatcher;
        public RegisterUserHandler(ICommandDispatcher commandDispatcher)
        {
            _dispatcher = commandDispatcher;
        }
        public async Task<Guid> HandleAsync(RegisterUserCommand command)
        {
            var authId = await _dispatcher.DispatchAsync<UserCreateCommand, Guid>(
              new UserCreateCommand(
               command.Email,
               command.Password
           )
            );

            await _dispatcher.DispatchAsync<CreateUserCommand, Guid>(
                new CreateUserCommand(
                     authId,
                     command.Name,
                     command.Email,
                     command.Phone

                )
            );
            return authId;
        }
    }
}
