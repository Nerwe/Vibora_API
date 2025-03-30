namespace Vibora_API.Contracts.Response.User
{
    public class UsersResponse
    {
        public IEnumerable<UserResponse> Users { get; set; } = [];
    }
}
