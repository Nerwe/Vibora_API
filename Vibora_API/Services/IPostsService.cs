using Vibora_API.Models.DTO;

namespace Vibora_API.Services
{
    public interface IPostsService
    {
        Task<PostDTO> AddPostAsync(PostDTO postDTO);
        Task<bool> DeletePostAsync(Guid id);
        Task<PostDTO?> GetPostByIdAsync(Guid id);
        Task<IEnumerable<PostDTO>?> GetPostsAsync();
        Task<IEnumerable<PostDTO>?> GetPostsByThreadAsync(Guid threadId);
        Task<IEnumerable<PostDTO>?> GetPostsByUserIdAsync(Guid userId);
        Task<PostDTO?> UpdatePostAsync(Guid id, PostDTO postDTO);
    }
}