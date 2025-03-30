namespace Vibora_API.Contracts.Response.Post
{
    public class PostsResponse
    {
        public IEnumerable<PostResponse> Posts { get; set; } = [];
    }
}
