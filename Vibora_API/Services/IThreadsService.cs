using Vibora_API.Models.DTO;

namespace Vibora_API.Services
{
    public interface IThreadsService
    {
        Task<ThreadDTO> AddThreadAsync(ThreadDTO threadDTO);
        Task<bool> DeleteThreadAsync(Guid id);
        Task<ThreadDTO?> GetThreadByIdAsync(Guid id);
        Task<IEnumerable<ThreadDTO>?> GetThreadsAsync();
        Task<ThreadDTO?> UpdateThreadAsync(Guid id, ThreadDTO threadDTO);
    }
}