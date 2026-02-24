using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using QUS.Core.Mediator.Commands;
using QUS.Workflows.API.DTOs;
using QUS.Workflows.Application.Registration.Commands;

namespace QUS.Workflows.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly ICommandDispatcher _dispatcher;

        public RegistrationController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationDTO registrationDTO)
        {
            var authId = Guid.NewGuid();
            var command = new AuthCreateCommand(authId,registrationDTO.Email,
                registrationDTO.Password);
           await _dispatcher.DispatchAsync<AuthCreateCommand, Guid>(command);
            var commandUser = new UserCreateCommand
                (registrationDTO.Name,registrationDTO.Email,
                registrationDTO.Phone, authId);
            await _dispatcher.DispatchAsync<UserCreateCommand, Guid>(commandUser);

            return Ok();
        }

    }
}
