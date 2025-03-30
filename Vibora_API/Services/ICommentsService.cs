using Vibora_API.Models.DTO;

namespace Vibora_API.Services
{
    public interface ICommentsService
    {
        Task<CommentDTO> AddCommentAsync(CommentDTO commentDTO);
        Task<bool> DeleteCommentAsync(Guid id);
        Task<CommentDTO?> GetCommentByIdAsync(Guid id);
        Task<IEnumerable<CommentDTO>?> GetCommentsAsync();
        Task<CommentDTO?> UpdateCommentAsync(Guid id, CommentDTO commentDTO);
    }
}