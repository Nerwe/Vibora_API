namespace Vibora_API.Contracts.Request.Thread
{
    public class CreateThreadRequest
    {
        public Guid UserID { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
