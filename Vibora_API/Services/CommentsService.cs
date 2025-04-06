using Vibora_API.Models.DTO;
using Vibora_API.Repositories;

namespace Vibora_API.Services
{
    public class CommentsService(ICommentsRepository commentsRepository) : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository = commentsRepository;

        public async Task<IEnumerable<CommentDTO>?> GetCommentsAsync()
        {
            return await _commentsRepository.GetAsync();
        }
        public async Task<IEnumerable<CommentDTO>?> GetCommentByPostIdAsync(Guid postId)
        {
            return await _commentsRepository.GetByPostIdAsync(postId);
        }
        public async Task<CommentDTO?> GetCommentByIdAsync(Guid id)
        {
            return await _commentsRepository.GetByIdAsync(id);
        }
        public async Task<CommentDTO> AddCommentAsync(CommentDTO commentDTO)
        {
            return await _commentsRepository.AddAsync(commentDTO);
        }
        public async Task<CommentDTO?> UpdateCommentAsync(Guid id, CommentDTO commentDTO)
        {
            return await _commentsRepository.UpdateAsync(id, commentDTO);
        }
        public async Task<bool> DeleteCommentAsync(Guid id)
        {
            return await _commentsRepository.DeleteAsync(id);
        }
    }
}
