using Microsoft.EntityFrameworkCore;
using SplitWiseAPI.DbContext;
using SplitWiseAPI.DTOs;
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

        public async Task<GroupResponseDTO> CreateGroupAsync(GroupCreateDTO groupDto)
        {
            var group = new Group { Name = groupDto.Name };
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return new GroupResponseDTO
            {
                Id = group.Id,
                Name = group.Name,
                Users = new List<UserResponseDTO>()
            };
        }

        public async Task<GroupResponseDTO?> GetGroupByIdAsync(Guid groupId)
        {
            var group = await _context.Groups
                .Include(g => g.Users)
                .Where(g => g.Id == groupId)
                .Select(g => new GroupResponseDTO
                {
                    Id = g.Id,
                    Name = g.Name,
                    Users = g.Users.Select(u => new UserResponseDTO
                    {
                        Id = u.Id,
                        Name = u.Name,
                        Email = u.Email
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return group;
        }


        public async Task<bool> AddUserToGroupAsync(Guid groupId, Guid userId)
        {
            var group = await _context.Groups.Include(g => g.Users).FirstOrDefaultAsync(g => g.Id == groupId);
            var user = await _context.Users.FindAsync(userId);

            if (group == null || user == null) return false;

            group.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveUserFromGroupAsync(Guid groupId, Guid userId)
        {
            var group = await _context.Groups.Include(g => g.Users).FirstOrDefaultAsync(g => g.Id == groupId);
            if (group == null) return false;

            var user = group.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return false;

            group.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<GroupResponseDTO>> GetAllGroupsAsync()
        {
            var groups = await _context.Groups.Include(g => g.Users).ToListAsync();

            return groups.Select(group => new GroupResponseDTO
            {
                Id = group.Id,
                Name = group.Name,
                Users = group.Users.Select(u => new UserResponseDTO
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email
                }).ToList()
            }).ToList();
        }
    }
}
