namespace Vibora_API.Contracts.Request.Post
{
    public class UpdatePostRequest
    {
        public Guid ID { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int Score { get; set; }
        public bool IsHidden { get; set; }
        public bool IsDeleted { get; set; }
    }
}
