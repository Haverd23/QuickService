using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using QUS.Core.Mediator.Commands;
using QUS.Workflows.API.DTOs;

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
       
    }
}
