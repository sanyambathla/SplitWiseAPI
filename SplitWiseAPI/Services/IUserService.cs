using SplitWiseAPI.Models;

namespace SplitWiseAPI.Services
{
    public interface IUserService
    {
        Task<User> AddUserAsync(string name, string email);
        Task<List<User>> GetAllUsersAsync();
        Task<bool> RemoveUserAsync(Guid userId);
        Task<User?> GetUserByIdAsync(Guid userId);
    }
}
