using SplitWiseAPI.Models;
using SplitWiseAPI.DbContext;  // Import your DbContext
using Microsoft.EntityFrameworkCore;

namespace SplitWiseAPI.Services
{
    public class UserService : IUserService
    {
        private readonly SplitwiseDbContext _context;

        public UserService(SplitwiseDbContext context) => _context = context;

        public async Task<User> AddUserAsync(string name, string email)
        {
            var user = new User { Name = name, Email = email };
            _context.Users.Add(user);                 // Add to Users table
            await _context.SaveChangesAsync();       // Save to database
            return user;
        }

        public async Task<bool> RemoveUserAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();      // Commit removal
            return true;
        }

        public async Task<List<User>> GetAllUsersAsync() =>
            await _context.Users.ToListAsync();      // Fetch from DB

        public async Task<User?> GetUserByIdAsync(Guid userId)
        {
            return await _context.Users.FindAsync(userId); // Assuming EF Core context
        }
    }
}