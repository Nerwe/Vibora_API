namespace Vibora_API.Models.DB
{
    public class User
    {
        public Guid ID { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime LastActiveDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Role> Roles { get; set; } = [];
        public ICollection<Post> Posts { get; set; } = [];
        public ICollection<Thread> Threads { get; set; } = [];
        public ICollection<Comment> Comments { get; set; } = [];
    }
}
