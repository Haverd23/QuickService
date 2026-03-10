using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QUS.Auth.API.DTOs;
using QUS.Auth.Application.Commands;
using QUS.Auth.Application.Interfaces;
using QUS.Auth.Application.Queries;
using QUS.Core.Mediator.Commands;
using QUS.Core.Mediator.Queries;

namespace QUS.Auth.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly ILogin _loginService;
        private readonly IQueryDispatcher _queryDispatcher;

        public AuthController(ICommandDispatcher commandDispatcher,
            ILogin loginService,
            IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _loginService = loginService;
            _queryDispatcher = queryDispatcher;
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
        [HttpGet("email-exists")]
        public async Task<IActionResult> CheckEmailExists([FromQuery] string email)
        {

            var query = new GetUserByEmailQuery(email);
            var exists = await _queryDispatcher.DispatchAsync<GetUserByEmailQuery, bool>(query);
            return Ok(new { Exists = exists });
        }
    }
}
