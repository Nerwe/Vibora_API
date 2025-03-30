using Vibora_API.Models.DTO;
using Vibora_API.Repositories;

namespace Vibora_API.Services
{
    public class RolesService(IRolesRepository rolesRepository) : IRolesService
    {
        private readonly IRolesRepository _rolesRepository = rolesRepository;
        public async Task<IEnumerable<RoleDTO>?> GetRolesAsync()
        {
            return await _rolesRepository.GetAsync();
        }
        public async Task<RoleDTO?> GetRoleByIdAsync(int id)
        {
            return await _rolesRepository.GetByIdAsync(id);
        }
        public async Task<RoleDTO> AddRoleAsync(RoleDTO roleDTO)
        {
            return await _rolesRepository.AddAsync(roleDTO);
        }
        public async Task<RoleDTO?> UpdateRoleAsync(int id, RoleDTO roleDTO)
        {
            return await _rolesRepository.UpdateAsync(id, roleDTO);
        }
        public async Task<bool> DeleteRoleAsync(int id)
        {
            return await _rolesRepository.DeleteAsync(id);
        }
    }
}
