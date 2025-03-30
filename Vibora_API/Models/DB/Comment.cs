namespace Vibora_API.Models.DB
{
    public class Comment
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid PostID { get; set; }
        public string Content { get; set; } = null!;
        public int Score { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsHidden { get; set; }
        public bool IsDeleted { get; set; }

        public User User { get; set; } = null!;
        public Post Post { get; set; } = null!;
    }
}
