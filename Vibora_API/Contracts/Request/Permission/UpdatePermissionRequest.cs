namespace Vibora_API.Contracts.Request.Permission
{
    public class UpdatePermissionRequest
    {
        public int ID { get; set; }
        public string Title { get; set; } = null!;
    }
}
