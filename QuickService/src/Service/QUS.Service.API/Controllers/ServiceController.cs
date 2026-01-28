using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QUS.Core.Mediator.Commands;
using QUS.Service.API.DTOs;
using QUS.Service.Application.Commands;
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
        public async Task<IActionResult> CreateService([FromBody] ServiceDTO serviceDTO )
        {
            var idString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(idString, out Guid userId))
            {
                return Unauthorized("Id do usuário inválido no token.");
            }
            var command = new CreateServiceCommand(
                serviceDTO.Title,
                serviceDTO.Description,
                serviceDTO.Price,
                userId,
                serviceDTO.Category
            );
            var serviceId = await _commandDispatcher.DispatchAsync<CreateServiceCommand, Guid>(command);
            return CreatedAtAction(nameof(CreateService), new { id = serviceId }, null);
        }
    }
}
