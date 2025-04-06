using Microsoft.EntityFrameworkCore;
using Vibora_API.Data;
using Vibora_API.Models.DB;
using Vibora_API.Models.DTO;

namespace Vibora_API.Repositories
{
    public class CommentsRepository(ViboraDBContext context) : ICommentsRepository
    {
        private readonly ViboraDBContext _context = context;

        public async Task<IEnumerable<CommentDTO>?> GetAsync()
        {
            var comments = await _context.Comments.ToListAsync();

            var commentDTOs = comments.Select(c => new CommentDTO
            {
                ID = c.ID,
                UserID = c.UserID,
                PostID = c.PostID,
                Content = c.Content,
                Score = c.Score,
                CreatedDate = c.CreatedDate,
                IsHidden = c.IsHidden,
                IsDeleted = c.IsDeleted
            });

            return commentDTOs;
        }

        public async Task<IEnumerable<CommentDTO>?> GetByPostIdAsync(Guid postId)
        {
            var comments = await _context.Comments.Where(c => c.PostID == postId).ToListAsync();
            var commentDTOs = comments.Select(c => new CommentDTO
            {
                ID = c.ID,
                UserID = c.UserID,
                PostID = c.PostID,
                Content = c.Content,
                Score = c.Score,
                CreatedDate = c.CreatedDate,
                IsHidden = c.IsHidden,
                IsDeleted = c.IsDeleted
            });
            return commentDTOs;
        }

        public async Task<CommentDTO?> GetByIdAsync(Guid id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.ID == id);

            if (comment == null) return null;

            var commentDTO = new CommentDTO
            {
                ID = comment.ID,
                UserID = comment.UserID,
                PostID = comment.PostID,
                Content = comment.Content,
                Score = comment.Score,
                CreatedDate = comment.CreatedDate,
                IsHidden = comment.IsHidden,
                IsDeleted = comment.IsDeleted
            };
            return commentDTO;
        }

        public async Task<CommentDTO> AddAsync(CommentDTO commentDTO)
        {
            var comment = new Comment
            {
                ID = commentDTO.ID,
                UserID = commentDTO.UserID,
                PostID = commentDTO.PostID,
                Content = commentDTO.Content,
                Score = commentDTO.Score,
                CreatedDate = commentDTO.CreatedDate,
                IsHidden = commentDTO.IsHidden,
                IsDeleted = commentDTO.IsDeleted
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            return commentDTO;
        }

        public async Task<CommentDTO?> UpdateAsync(Guid id, CommentDTO commentDTO)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.ID == id);

            if (comment == null) return null;
            comment.UserID = commentDTO.UserID;
            comment.PostID = commentDTO.PostID;
            comment.Content = commentDTO.Content;
            comment.Score = commentDTO.Score;
            comment.CreatedDate = commentDTO.CreatedDate;
            comment.IsHidden = commentDTO.IsHidden;
            comment.IsDeleted = commentDTO.IsDeleted;
            await _context.SaveChangesAsync();
            return commentDTO;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.ID == id);
            if (comment == null) return false;
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
