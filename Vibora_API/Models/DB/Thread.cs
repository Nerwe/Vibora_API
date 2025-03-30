namespace Vibora_API.Models.DB
{
    public class Thread
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsHidden { get; set; }
        public bool IsDeleted { get; set; }

        public User User { get; set; } = null!;
        public ICollection<Post> Posts { get; set; } = [];
    }
}
