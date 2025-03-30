using Vibora_API.Models.DTO;

namespace Vibora_API.Repositories
{
    public interface IPostsRepository
    {
        Task<PostDTO> AddAsync(PostDTO postDTO);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<PostDTO>?> GetAsync();
        Task<PostDTO?> GetByIdAsync(Guid id);
        Task<PostDTO?> UpdateAsync(Guid id, PostDTO postDTO);
    }
}