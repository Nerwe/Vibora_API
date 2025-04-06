using Vibora_API.Models.DTO;

namespace Vibora_API.Repositories
{
    public interface IThreadsRepository
    {
        Task<ThreadDTO> AddAsync(ThreadDTO threadDTO);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<ThreadDTO>?> GetAsync();
        Task<ThreadDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<ThreadDTO>?> GetByUserIdAsync(Guid userId);
        Task<ThreadDTO?> UpdateAsync(Guid id, ThreadDTO threadDTO);
    }
}