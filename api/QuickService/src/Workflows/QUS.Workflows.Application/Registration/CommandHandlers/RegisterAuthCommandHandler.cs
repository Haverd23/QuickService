using QUS.Core.Mediator.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QUS.Workflows.Application.Registration.CommandHandlers
{
    public class RegisterAuthCommandHandler : ICommandHandler<RegisterUserCommand, Guid>
    {
        public async Task<Guid> HandleAsync(RegisterUserCommand command)
        {
            var authId = Guid.NewGuid();
            return await Task.FromResult(authId);
        }
    }
}
