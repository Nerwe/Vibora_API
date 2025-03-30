namespace Vibora_API.Contracts.Request.Comment
{
    public class UpdateCommentRequest
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid PostID { get; set; }
        public string Content { get; set; } = null!;
        public int Score { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsHidden { get; set; }
        public bool IsDeleted { get; set; }
    }
}
