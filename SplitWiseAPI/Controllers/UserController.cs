using Microsoft.AspNetCore.Mvc;
using SplitWiseAPI.Models;
using SplitWiseAPI.Services;

namespace SplitWiseAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) => _userService = userService;

        [HttpPost("add")]
        public async Task<IActionResult> AddUser(User user)
        {
            var newUser = await _userService.AddUserAsync(user.Name, user.Email);
            return CreatedAtAction(nameof(AddUser), new { id = newUser.Id }, newUser); // 201 Created with location
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveUser(Guid id)
        {
            var removed = await _userService.RemoveUserAsync(id);
            return removed ? Ok("User removed.") : NotFound("User not found.");
        }
    }
}
