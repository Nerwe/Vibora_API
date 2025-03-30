namespace Vibora_API.Models.DB
{
    public class Permission
    {
        public int ID { get; set; }
        public string Title { get; set; } = null!;

        public ICollection<Role> Roles { get; } = [];
    }
}
