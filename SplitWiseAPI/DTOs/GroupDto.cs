using SplitWiseAPI.Models;

namespace SplitWiseAPI.DTOs
{
    public class GroupCreateDTO
    {
        public string Name { get; set; }
        public List<Guid> UserIds { get; set; }
    }

    public class GroupResponseDTO
    {
        public GroupResponseDTO()
        {
           
        }
        public GroupResponseDTO(Group group)
        {
            Id = group.Id;
            Name = group.Name;
            Users = group.Users?.Select(u => new UserResponseDTO(u)).ToList() ?? new List<UserResponseDTO>();
        }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<UserResponseDTO> Users { get; set; }
    }

}
