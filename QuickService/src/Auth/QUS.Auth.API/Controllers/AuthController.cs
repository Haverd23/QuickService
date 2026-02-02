using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QUS.Auth.API.DTOs;
using QUS.Auth.Application.Commands;
using QUS.Auth.Application.Interfaces;
using QUS.Core.Mediator.Commands;

namespace QUS.Auth.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ILogin _loginService;

        public AuthController(ICommandDispatcher commandDispatcher,
            ILogin loginService)
        {
            _commandDispatcher = commandDispatcher;
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO request)
        {
            var command = new UserCreateCommand(request.Email, request.Password);
            var userId = await _commandDispatcher.DispatchAsync<UserCreateCommand, Guid>(command);
            return CreatedAtAction(nameof(CreateUser), new { id = userId }, null);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDTO request)
        {
            var token = await _loginService.Authenticate(request.Email, request.Password);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(new { Token = token });
        }

    }
}
