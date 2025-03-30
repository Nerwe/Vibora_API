using Vibora_API.Models.DTO;

namespace Vibora_API.Repositories
{
    public interface IRolesRepository
    {
        Task<RoleDTO> AddAsync(RoleDTO roleDTO);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<RoleDTO>?> GetAsync();
        Task<RoleDTO?> GetByIdAsync(int id);
        Task<RoleDTO?> UpdateAsync(int id, RoleDTO roleDTO);
    }
}