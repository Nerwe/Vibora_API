namespace Vibora_API.Contracts.Response.Comment
{
    public class CommentResponse
    {
        public Guid ID { get; set; }
        public Guid PostID { get; set; }
        public Guid UserID { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public int Score { get; set; }
        public bool IsHidden { get; set; }
        public bool IsDeleted { get; set; }
    }
}
