using Microsoft.EntityFrameworkCore;
using SplitWiseAPI.DbContext;
using SplitWiseAPI.Models;

namespace SplitWiseAPI.Services
{
    public class GroupService : IGroupService
    {
        private readonly SplitwiseDbContext _context;

        public GroupService(SplitwiseDbContext context)
        {
            _context = context;
        }

        public async Task<Group> CreateGroupAsync(string name)
        {
            var group = new Group { Name = name };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
            return group;
        }

        public async Task<bool> AddUserToGroupAsync(Guid groupId, User user)
        {
            var group = await _context.Groups.Include(g => g.Users).FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null)
                return false;

            group.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveUserFromGroupAsync(Guid groupId, Guid userId)
        {
            var group = await _context.Groups.Include(g => g.Users).FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null)
                return false;

            var user = group.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
                return false;

            group.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Group>> GetAllGroupsAsync()
        {
            return await _context.Groups.Include(g => g.Users).ToListAsync();
        }
    }
}
