namespace Vibora_API.Contracts.Response.Comment
{
    public class CommentsResponse
    {
        public IEnumerable<CommentResponse> Comments { get; set; } = [];
    }
}
