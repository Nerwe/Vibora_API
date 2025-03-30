namespace Vibora_API.Models.DB
{
    public class Post
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid ThreadID { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int Score { get; set; }
        public bool IsHidden { get; set; }
        public bool IsDeleted { get; set; }

        public User User { get; set; } = null!;
        public Thread Thread { get; set; } = null!;
        public ICollection<Comment> Comments { get; set; } = [];
    }
}
