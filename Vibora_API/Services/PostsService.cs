using Vibora_API.Models.DTO;
using Vibora_API.Repositories;

namespace Vibora_API.Services
{
    public class PostsService(IPostsRepository postsRepository) : IPostsService
    {
        private readonly IPostsRepository _postsRepository = postsRepository;
        public async Task<IEnumerable<PostDTO>?> GetPostsAsync()
        {
            return await _postsRepository.GetAsync();
        }
        public async Task<IEnumerable<PostDTO>?> GetPostsByUserIdAsync(Guid userId)
        {
            return await _postsRepository.GetByUserIdAsync(userId);
        }
        public async Task<PostDTO?> GetPostByIdAsync(Guid id)
        {
            return await _postsRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<PostDTO>?> GetPostsByThreadAsync(Guid threadId)
        {
            return await _postsRepository.GetByThreadIdAsync(threadId);
        }
        public async Task<PostDTO> AddPostAsync(PostDTO postDTO)
        {
            return await _postsRepository.AddAsync(postDTO);
        }
        public async Task<PostDTO?> UpdatePostAsync(Guid id, PostDTO postDTO)
        {
            return await _postsRepository.UpdateAsync(id, postDTO);
        }
        public async Task<bool> DeletePostAsync(Guid id)
        {
            return await _postsRepository.DeleteAsync(id);
        }
    }
}
