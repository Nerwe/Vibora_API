using Microsoft.EntityFrameworkCore;
using Vibora_API.Data;
using Vibora_API.Models.DB;
using Vibora_API.Models.DTO;

namespace Vibora_API.Repositories
{
    public class RolesRepository(ViboraDBContext context) : IRolesRepository
    {
        private readonly ViboraDBContext _context = context;

        public async Task<IEnumerable<RoleDTO>?> GetAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            var roleDTOs = roles.Select(r => new RoleDTO
            {
                ID = r.ID,
                Title = r.Title
            });
            return roleDTOs;
        }

        public async Task<RoleDTO?> GetByIdAsync(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.ID == id);
            if (role == null) return null;
            var roleDTO = new RoleDTO
            {
                ID = role.ID,
                Title = role.Title
            };
            return roleDTO;
        }

        public async Task<RoleDTO> AddAsync(RoleDTO roleDTO)
        {
            var role = new Role
            {
                ID = roleDTO.ID,
                Title = roleDTO.Title
            };
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return roleDTO;
        }

        public async Task<RoleDTO?> UpdateAsync(int id, RoleDTO roleDTO)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.ID == id);
            if (role == null) return null;
            role.Title = roleDTO.Title;
            return roleDTO;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(r => r.ID == id);
            if (role == null) return false;
            _context.Roles.Remove(role);
            return true;
        }
    }
}
