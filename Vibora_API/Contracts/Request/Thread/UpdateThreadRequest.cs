namespace Vibora_API.Contracts.Request.Thread
{
    public class UpdateThreadRequest
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsHidden { get; set; }
        public bool IsDeleted { get; set; }
    }
}
