using Vibora_API.Contracts.Request.Permission;
using Vibora_API.Contracts.Response.Permission;
using Vibora_API.Models.DTO;

namespace Vibora_API.Mappers
{
    public static class PermissionMapper
    {
        public static PermissionDTO ToDTO(this CreatePermissionRequest request)
        {
            return new PermissionDTO
            {
                Title = request.Title
            };
        }

        public static PermissionDTO ToDTO(this UpdatePermissionRequest request)
        {
            return new PermissionDTO
            {
                ID = request.ID,
                Title = request.Title
            };
        }

        public static PermissionResponse ToResponse(this PermissionDTO dto)
        {
            return new PermissionResponse
            {
                ID = dto.ID,
                Title = dto.Title
            };
        }

        public static PermissionDTO ToDTO(this PermissionResponse response)
        {
            return new PermissionDTO
            {
                ID = response.ID,
                Title = response.Title
            };
        }
    }
}
