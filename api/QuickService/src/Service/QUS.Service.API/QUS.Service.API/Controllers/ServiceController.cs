using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QUS.Core.Mediator.Commands;
using QUS.Service.API.DTOs;
using QUS.Service.Application.Commands;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QUS.Service.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        public ServiceController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ServiceDTO commandDTO)
        {
            var idString =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                User.FindFirst(JwtRegisteredClaimNames.NameId)?.Value ??
                User.FindFirst("nameid")?.Value;

            if (!Guid.TryParse(idString, out Guid userId))
            {
                return Unauthorized(new { message = "Id do usuário inválido no token." });
            }
            var command = new CreateServiceCommand(
                commandDTO.Title,
                commandDTO.Description,
                commandDTO.Price,
                userId,
                commandDTO.Category,
                commandDTO.City

            );
            var serviceId = await _commandDispatcher.DispatchAsync<CreateServiceCommand, Guid>(command);
            return CreatedAtAction(nameof(Post), new { id = serviceId }, null);
        }
    }
}
