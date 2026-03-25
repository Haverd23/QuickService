using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QUS.Core.Mediator.Commands;
using QUS.Core.Mediator.Queries;
using QUS.Service.API.DTOs;
using QUS.Service.Application.Commands;
using QUS.Service.Application.DTOs;
using QUS.Service.Application.Queries;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QUS.Service.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;
        public ServiceController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }
        [Authorize]
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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllServicesQuery();
            var services = await _queryDispatcher.DispatchAsync<GetAllServicesQuery, IEnumerable<AllServicesDTO>>(query);
            return Ok(services);
        }
    }
}
