using Vibora_API.Contracts.Request.Role;
using Vibora_API.Contracts.Response.Role;
using Vibora_API.Models.DTO;

namespace Vibora_API.Mappers
{
    public static class RoleMapper
    {
        public static RoleDTO ToDTO(this CreateRoleRequest request)
        {
            return new RoleDTO
            {
                Title = request.Title
            };
        }

        public static RoleDTO ToDTO(this UpdateRoleRequest request)
        {
            return new RoleDTO
            {
                ID = request.ID,
                Title = request.Title
            };
        }

        public static RoleResponse ToResponse(this RoleDTO dto)
        {
            return new RoleResponse
            {
                ID = dto.ID,
                Title = dto.Title
            };
        }

        public static RoleDTO ToDTO(this RoleResponse response)
        {
            return new RoleDTO
            {
                ID = response.ID,
                Title = response.Title
            };
        }
    }
}
