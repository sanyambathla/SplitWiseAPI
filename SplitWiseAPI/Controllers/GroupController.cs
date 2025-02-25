using Microsoft.AspNetCore.Mvc;
using SplitWiseAPI.Services;

namespace SplitWiseAPI.Controllers
{
    [ApiController]
    [Route("api/group")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;
        private readonly IUserService _userService;

        public GroupController(IGroupService groupService, IUserService userService)
        {
            _groupService = groupService;
            _userService = userService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGroup(string name)
        {
            var group = await _groupService.CreateGroupAsync(name);
            return CreatedAtAction(nameof(CreateGroup), new { groupId = group.Id }, group);
        }

        [HttpPost("{groupId}/add-user/{userId}")]
        public async Task<IActionResult> AddUserToGroup(Guid groupId, Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound("User not found.");

            var added = await _groupService.AddUserToGroupAsync(groupId, user);
            return added
                ? CreatedAtAction(nameof(AddUserToGroup), new { groupId, userId }, "User added to group.")
                : NotFound("Group not found.");
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = await _groupService.GetAllGroupsAsync();
            return Ok(groups);
        }
    }
}
