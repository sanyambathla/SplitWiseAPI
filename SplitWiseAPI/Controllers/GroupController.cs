using Microsoft.AspNetCore.Mvc;
using SplitWiseAPI.DTOs;
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
        public async Task<IActionResult> CreateGroup([FromBody] GroupCreateDTO groupDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var groupResponse = await _groupService.CreateGroupAsync(groupDto); // Pass the entire DTO

            return CreatedAtAction(nameof(CreateGroup), new { groupId = groupResponse.Id }, groupResponse);
        }

        [HttpPost("{groupId}/add-user/{userId}")]
        public async Task<IActionResult> AddUserToGroup(Guid groupId, Guid userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) return NotFound("User not found.");

            var added = await _groupService.AddUserToGroupAsync(groupId, userId);
            if (!added) return NotFound("Group not found.");

            var groupResponse = await _groupService.GetGroupByIdAsync(groupId);

            return CreatedAtAction(nameof(AddUserToGroup), new { groupId, userId }, groupResponse);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllGroups()
        {
            var response = await _groupService.GetAllGroupsAsync();
            return Ok(response);
        }
    }
}
