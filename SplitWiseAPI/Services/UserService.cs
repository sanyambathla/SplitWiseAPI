using SplitWiseAPI.Models;
using SplitWiseAPI.DbContext;
using Microsoft.EntityFrameworkCore;
using SplitWiseAPI.DTOs;

namespace SplitWiseAPI.Services
{
    public class UserService : IUserService
    {
        private readonly SplitwiseDbContext _context;

        public UserService(SplitwiseDbContext context) => _context = context;

        public async Task<UserResponseDTO> AddUserAsync(UserCreateDTO userDto)
        {
            var user = new User { Name = userDto.Name, Email = userDto.Email };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserResponseDTO(user);  // Map User to DTO
        }

        public async Task<bool> RemoveUserAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<UserResponseDTO>> GetAllUsersAsync() =>
            await _context.Users
                          .Select(u => new UserResponseDTO(u)) // Map each user to DTO
                          .ToListAsync();

        public async Task<UserResponseDTO?> GetUserByIdAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user == null ? null : new UserResponseDTO(user); // Return DTO
        }
    }
}
