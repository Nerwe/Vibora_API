using Vibora_API.Models.DTO;
using Vibora_API.Repositories;

namespace Vibora_API.Services
{
    public class PermissionsService(IPermissionsRepository permissionsRepository) : IPermissionsService
    {
        private readonly IPermissionsRepository _permissionsRepository = permissionsRepository;

        public async Task<IEnumerable<PermissionDTO>?> GetPermissionsAsync()
        {
            return await _permissionsRepository.GetAsync();
        }
        public async Task<PermissionDTO?> GetPermissionByIdAsync(int id)
        {
            return await _permissionsRepository.GetByIdAsync(id);
        }
        public async Task<PermissionDTO> AddPermissionAsync(PermissionDTO permissionDTO)
        {
            return await _permissionsRepository.AddAsync(permissionDTO);
        }
        public async Task<PermissionDTO?> UpdatePermissionAsync(int id, PermissionDTO permissionDTO)
        {
            return await _permissionsRepository.UpdateAsync(id, permissionDTO);
        }
        public async Task<bool> DeletePermissionAsync(int id)
        {
            return await _permissionsRepository.DeleteAsync(id);
        }
    }
}
