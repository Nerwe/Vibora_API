using Microsoft.EntityFrameworkCore;
using Vibora_API.Data;
using Vibora_API.Models.DB;
using Vibora_API.Models.DTO;

namespace Vibora_API.Repositories
{
    public class PermissionsRepository(ViboraDBContext context) : IPermissionsRepository
    {
        private readonly ViboraDBContext _context = context;
        public async Task<IEnumerable<PermissionDTO>?> GetAsync()
        {
            var permissions = await _context.Permissions.ToListAsync();
            var permissionDTOs = permissions.Select(p => new PermissionDTO
            {
                ID = p.ID,
                Title = p.Title
            });
            return permissionDTOs;
        }
        public async Task<PermissionDTO?> GetByIdAsync(int id)
        {
            var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.ID == id);
            if (permission == null) return null;
            var permissionDTO = new PermissionDTO
            {
                ID = permission.ID,
                Title = permission.Title
            };
            return permissionDTO;
        }
        public async Task<PermissionDTO> AddAsync(PermissionDTO permissionDTO)
        {
            var permission = new Permission
            {
                ID = permissionDTO.ID,
                Title = permissionDTO.Title
            };
            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();
            return permissionDTO;
        }
        public async Task<PermissionDTO?> UpdateAsync(int id, PermissionDTO permissionDTO)
        {
            var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.ID == id);
            if (permission == null) return null;
            permission.Title = permissionDTO.Title;
            await _context.SaveChangesAsync();
            return permissionDTO;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.ID == id);
            if (permission == null) return false;
            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
