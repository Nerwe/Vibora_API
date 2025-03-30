namespace Vibora_API.Models.DTO
{
    public class UserDTO
    {
        public Guid ID { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public ICollection<RoleDTO> Roles { get; set; } = [];
        public DateTime CreatedDate { get; set; }
        public DateTime LastActiveDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
