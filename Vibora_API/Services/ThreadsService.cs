using Vibora_API.Models.DTO;
using Vibora_API.Repositories;

namespace Vibora_API.Services
{
    public class ThreadsService(IThreadsRepository threadsRepository) : IThreadsService
    {
        private readonly IThreadsRepository _threadsRepository = threadsRepository;
        public async Task<IEnumerable<ThreadDTO>?> GetThreadsAsync()
        {
            return await _threadsRepository.GetAsync();
        }
        public async Task<ThreadDTO?> GetThreadByIdAsync(Guid id)
        {
            return await _threadsRepository.GetByIdAsync(id);
        }
        public async Task<ThreadDTO> AddThreadAsync(ThreadDTO threadDTO)
        {
            return await _threadsRepository.AddAsync(threadDTO);
        }
        public async Task<ThreadDTO?> UpdateThreadAsync(Guid id, ThreadDTO threadDTO)
        {
            return await _threadsRepository.UpdateAsync(id, threadDTO);
        }
        public async Task<bool> DeleteThreadAsync(Guid id)
        {
            return await _threadsRepository.DeleteAsync(id);
        }
    }
}
