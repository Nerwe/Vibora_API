namespace Vibora_API.Models.DTO
{
    public class PostDTO
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
    }
}
