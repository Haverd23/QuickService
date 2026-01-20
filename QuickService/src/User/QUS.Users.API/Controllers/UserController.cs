using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QUS.Users.API.DTOs;
using QUS.Users.Application;

namespace QUS.Users.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserDTO userDto)
        {
             _userService.CreateUser(userDto.Name,userDto.Email,userDto.Phone);
            return Created();
        }
    }
}
