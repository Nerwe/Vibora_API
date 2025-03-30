namespace Vibora_API.Contracts.Response.Thread
{
    public class ThreadsResponse
    {
        public IEnumerable<ThreadResponse> Threads { get; set; } = [];
    }
}
