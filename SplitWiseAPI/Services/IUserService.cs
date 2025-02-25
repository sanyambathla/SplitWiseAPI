using SplitWiseAPI.DTOs;
using SplitWiseAPI.Models;

namespace SplitWiseAPI.Services
{
    public interface IUserService
    {
        Task<UserResponseDTO> AddUserAsync(UserCreateDTO userDto);
        Task<List<UserResponseDTO>> GetAllUsersAsync();
        Task<bool> RemoveUserAsync(Guid userId);
        Task<UserResponseDTO?> GetUserByIdAsync(Guid userId);
    }
}
