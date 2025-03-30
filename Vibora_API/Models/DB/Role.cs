namespace Vibora_API.Models.DB
{
    public class Role
    {
        public int ID { get; set; }
        public string Title { get; set; } = null!;

        public ICollection<User> Users { get; } = [];
        public ICollection<Permission> Permissions { get; } = [];
    }
}
