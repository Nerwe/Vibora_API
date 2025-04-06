using Microsoft.EntityFrameworkCore;
using Vibora_API.Data;
using Vibora_API.Models.DTO;

namespace Vibora_API.Repositories
{
    public class ThreadsRepository(ViboraDBContext context) : IThreadsRepository
    {
        private readonly ViboraDBContext _context = context;

        public async Task<IEnumerable<ThreadDTO>?> GetAsync()
        {
            var threads = await _context.Threads.ToListAsync();
            var threadDTOs = threads.Select(t => new ThreadDTO
            {
                ID = t.ID,
                UserID = t.UserID,
                Title = t.Title,
                Description = t.Description,
                IsHidden = t.IsHidden,
                IsDeleted = t.IsDeleted
            });
            return threadDTOs;
        }

        public async Task<ThreadDTO?> GetByIdAsync(Guid id)
        {
            var thread = await _context.Threads.FirstOrDefaultAsync(t => t.ID == id);
            if (thread == null) return null;
            var threadDTO = new ThreadDTO
            {
                ID = thread.ID,
                UserID = thread.UserID,
                Title = thread.Title,
                Description = thread.Description,
                IsHidden = thread.IsHidden,
                IsDeleted = thread.IsDeleted
            };
            return threadDTO;
        }

        public async Task<IEnumerable<ThreadDTO>?> GetByUserIdAsync(Guid userId)
        {
            var threads = await _context.Threads.Where(t => t.UserID == userId).ToListAsync();
            var threadDTOs = threads.Select(t => new ThreadDTO
            {
                ID = t.ID,
                UserID = t.UserID,
                Title = t.Title,
                Description = t.Description,
                IsHidden = t.IsHidden,
                IsDeleted = t.IsDeleted
            });
            return threadDTOs;
        }

        public async Task<ThreadDTO> AddAsync(ThreadDTO threadDTO)
        {
            var thread = new Models.DB.Thread
            {
                ID = threadDTO.ID,
                UserID = threadDTO.UserID,
                Title = threadDTO.Title,
                Description = threadDTO.Description,
                IsHidden = threadDTO.IsHidden,
                IsDeleted = threadDTO.IsDeleted
            };
            await _context.Threads.AddAsync(thread);
            await _context.SaveChangesAsync();
            return threadDTO;
        }

        public async Task<ThreadDTO?> UpdateAsync(Guid id, ThreadDTO threadDTO)
        {
            var thread = await _context.Threads.FirstOrDefaultAsync(t => t.ID == id);
            if (thread == null) return null;
            thread.Title = threadDTO.Title;
            thread.Description = threadDTO.Description;
            thread.IsHidden = threadDTO.IsHidden;
            thread.IsDeleted = threadDTO.IsDeleted;
            return threadDTO;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var thread = await _context.Threads.FirstOrDefaultAsync(t => t.ID == id);
            if (thread == null) return false;
            _context.Threads.Remove(thread);
            return true;
        }
    }
}
