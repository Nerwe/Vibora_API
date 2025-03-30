namespace Vibora_API.Contracts.Request.Post
{
    public class CreatePostRequest
    {
        public Guid UserID { get; set; }
        public Guid ThreadID { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}
