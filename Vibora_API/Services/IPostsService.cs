using Vibora_API.Models.DTO;

namespace Vibora_API.Services
{
    public interface IPostsService
    {
        Task<PostDTO> AddPostAsync(PostDTO postDTO);
        Task<bool> DeletePostAsync(Guid id);
        Task<PostDTO?> GetPostByIdAsync(Guid id);
        Task<IEnumerable<PostDTO>?> GetPostsAsync();
        Task<PostDTO?> UpdatePostAsync(Guid id, PostDTO postDTO);
    }
}