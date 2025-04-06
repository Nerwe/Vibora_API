using Vibora_API.Models.DTO;

namespace Vibora_API.Repositories
{
    public interface ICommentsRepository
    {
        Task<CommentDTO> AddAsync(CommentDTO commentDTO);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<CommentDTO>?> GetAsync();
        Task<CommentDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<CommentDTO>?> GetByPostIdAsync(Guid postId);
        Task<CommentDTO?> UpdateAsync(Guid id, CommentDTO commentDTO);
    }
}