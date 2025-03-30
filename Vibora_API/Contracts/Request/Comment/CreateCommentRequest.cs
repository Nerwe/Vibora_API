namespace Vibora_API.Contracts.Request.Comment
{
    public class CreateCommentRequest
    {
        public Guid UserID { get; set; }
        public Guid PostID { get; set; }
        public string Content { get; set; } = null!;
    }
}
