using SplitWiseAPI.Models;

namespace SplitWiseAPI.DTOs
{
    public class UserCreateDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class UserResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public UserResponseDTO() { }  // Parameterless constructor for deserialization

        public UserResponseDTO(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
        }
    }
}
