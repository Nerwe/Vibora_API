using Vibora_API.Models.DTO;

namespace Vibora_API.Repositories
{
    public interface IPermissionsRepository
    {
        Task<PermissionDTO> AddAsync(PermissionDTO permissionDTO);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<PermissionDTO>?> GetAsync();
        Task<PermissionDTO?> GetByIdAsync(int id);
        Task<PermissionDTO?> UpdateAsync(int id, PermissionDTO permissionDTO);
    }
}