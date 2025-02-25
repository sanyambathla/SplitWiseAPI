using SplitWiseAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SplitWiseAPI.Services
{
    public interface IGroupService
    {
        Task<Group> CreateGroupAsync(string name);
        Task<bool> AddUserToGroupAsync(Guid groupId, User user);
        Task<bool> RemoveUserFromGroupAsync(Guid groupId, Guid userId);
        Task<List<Group>> GetAllGroupsAsync();
    }
}