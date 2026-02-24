
using Microsoft.AspNetCore.Mvc;
using QUS.Core.Mediator.Commands;
using QUS.Users.API.DTOs;
using QUS.Users.Application.Commands;

namespace QUS.Users.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public UserController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
        {
             var command  = new CreateUserCommand(Guid.NewGuid(),userDto.Name, userDto.Email, userDto.Phone);
             var userId = await _commandDispatcher.DispatchAsync<CreateUserCommand, Guid>(command);
             return CreatedAtAction(nameof(CreateUser), new { id = userId }, null);

        }
    }
}
