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
            if (registrationDTO == null)
            {
                return BadRequest();
            }
            var authId = Guid.NewGuid();
            var command = new RegistrationCommand(authId,registrationDTO.Email,
                registrationDTO.Password, registrationDTO.Name, registrationDTO.Phone);
          
            var result = await _dispatcher.DispatchAsync<RegistrationCommand, Guid>(command);
            return Ok(result);

        }

    }
}
