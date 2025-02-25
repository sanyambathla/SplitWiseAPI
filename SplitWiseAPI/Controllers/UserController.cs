using Microsoft.AspNetCore.Mvc;
using SplitWiseAPI.DTOs;
using SplitWiseAPI.Services;
using System.Linq;

namespace SplitWiseAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] UserCreateDTO userDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userResponse = await _userService.AddUserAsync(userDto);

            return CreatedAtAction(nameof(AddUser), new { id = userResponse.Id }, userResponse);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _userService.GetAllUsersAsync();

            return Ok(response);
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            var removed = await _userService.RemoveUserAsync(id);
            return removed ? Ok("User removed.") : NotFound("User not found.");
        }
    }
}
