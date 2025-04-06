using Microsoft.EntityFrameworkCore;
using Vibora_API.Data;
using Vibora_API.Models.DB;
using Vibora_API.Models.DTO;

namespace Vibora_API.Repositories
{
    public class PostsRepository(ViboraDBContext context) : IPostsRepository
    {
        private readonly ViboraDBContext _context = context;

        public async Task<IEnumerable<PostDTO>?> GetAsync()
        {
            var posts = await _context.Posts.ToListAsync();
            var postDTOs = posts.Select(p => new PostDTO
            {
                ID = p.ID,
                UserID = p.UserID,
                ThreadID = p.ThreadID,
                Title = p.Title,
                Content = p.Content,
                Score = p.Score,
                CreatedDate = p.CreatedDate,
                LastUpdatedDate = p.LastUpdatedDate,
                IsHidden = p.IsHidden,
                IsDeleted = p.IsDeleted
            });
            return postDTOs;
        }

        public async Task<PostDTO?> GetByIdAsync(Guid id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.ID == id);
            if (post == null) return null;
            var postDTO = new PostDTO
            {
                ID = post.ID,
                UserID = post.UserID,
                ThreadID = post.ThreadID,
                Title = post.Title,
                Content = post.Content,
                Score = post.Score,
                CreatedDate = post.CreatedDate,
                LastUpdatedDate = post.LastUpdatedDate,
                IsHidden = post.IsHidden,
                IsDeleted = post.IsDeleted
            };
            return postDTO;
        }

        public async Task<IEnumerable<PostDTO>?> GetByUserIdAsync(Guid userId)
        {
            var posts = await _context.Posts.Where(p => p.UserID == userId).ToListAsync();
            var postDTOs = posts.Select(p => new PostDTO
            {
                ID = p.ID,
                UserID = p.UserID,
                ThreadID = p.ThreadID,
                Title = p.Title,
                Content = p.Content,
                Score = p.Score,
                CreatedDate = p.CreatedDate,
                LastUpdatedDate = p.LastUpdatedDate,
                IsHidden = p.IsHidden,
                IsDeleted = p.IsDeleted
            });
            return postDTOs;
        }

        public async Task<IEnumerable<PostDTO>?> GetByThreadIdAsync(Guid threadId)
        {
            var posts = await _context.Posts.Where(p => p.ThreadID == threadId).ToListAsync();
            var postDTOs = posts.Select(p => new PostDTO
            {
                ID = p.ID,
                UserID = p.UserID,
                ThreadID = p.ThreadID,
                Title = p.Title,
                Content = p.Content,
                Score = p.Score,
                CreatedDate = p.CreatedDate,
                LastUpdatedDate = p.LastUpdatedDate,
                IsHidden = p.IsHidden,
                IsDeleted = p.IsDeleted
            });
            return postDTOs;
        }
        public async Task<PostDTO> AddAsync(PostDTO postDTO)
        {
            var post = new Post
            {
                ID = postDTO.ID,
                UserID = postDTO.UserID,
                ThreadID = postDTO.ThreadID,
                Title = postDTO.Title,
                Content = postDTO.Content,
                Score = postDTO.Score,
                CreatedDate = postDTO.CreatedDate,
                LastUpdatedDate = postDTO.LastUpdatedDate,
                IsHidden = postDTO.IsHidden,
                IsDeleted = postDTO.IsDeleted
            };
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
            return postDTO;
        }

        public async Task<PostDTO?> UpdateAsync(Guid id, PostDTO postDTO)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.ID == id);
            if (post == null) return null;
            post.Title = postDTO.Title;
            post.Content = postDTO.Content;
            post.Score = postDTO.Score;
            post.IsHidden = postDTO.IsHidden;
            post.IsDeleted = postDTO.IsDeleted;
            await _context.SaveChangesAsync();
            return postDTO;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.ID == id);
            if (post == null) return false;
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
