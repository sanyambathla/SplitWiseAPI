using SplitWiseAPI.DTOs;
using SplitWiseAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SplitWiseAPI.Services
{
    public interface IGroupService
    {
        Task<GroupResponseDTO> CreateGroupAsync(GroupCreateDTO groupDto);
        Task<bool> AddUserToGroupAsync(Guid groupId, Guid userId);
        Task<bool> RemoveUserFromGroupAsync(Guid groupId, Guid userId);
        Task<List<GroupResponseDTO>> GetAllGroupsAsync();
        Task<GroupResponseDTO?> GetGroupByIdAsync(Guid groupId);
    }
}