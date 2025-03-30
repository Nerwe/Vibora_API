using Vibora_API.Models.DTO;

namespace Vibora_API.Services
{
    public interface IPermissionsService
    {
        Task<PermissionDTO> AddPermissionAsync(PermissionDTO permissionDTO);
        Task<bool> DeletePermissionAsync(int id);
        Task<PermissionDTO?> GetPermissionByIdAsync(int id);
        Task<IEnumerable<PermissionDTO>?> GetPermissionsAsync();
        Task<PermissionDTO?> UpdatePermissionAsync(int id, PermissionDTO permissionDTO);
    }
}