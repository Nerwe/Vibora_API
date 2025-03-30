namespace Vibora_API.Contracts.Request.Role
{
    public class UpdateRoleRequest
    {
        public int ID { get; set; }
        public string Title { get; set; } = null!;
    }
}
