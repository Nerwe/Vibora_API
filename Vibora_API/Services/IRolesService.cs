using Vibora_API.Models.DTO;

namespace Vibora_API.Services
{
    public interface IRolesService
    {
        Task<RoleDTO> AddRoleAsync(RoleDTO roleDTO);
        Task<bool> DeleteRoleAsync(int id);
        Task<RoleDTO?> GetRoleByIdAsync(int id);
        Task<IEnumerable<RoleDTO>?> GetRolesAsync();
        Task<RoleDTO?> UpdateRoleAsync(int id, RoleDTO roleDTO);
    }
}