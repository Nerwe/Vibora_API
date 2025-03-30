using Vibora_API.Contracts.Request.User;
using Vibora_API.Contracts.Response.User;
using Vibora_API.Models.DTO;

namespace Vibora_API.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToDTO(this CreateUserRequest request)
        {
            return new UserDTO
            {
                ID = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
            };
        }

        public static UserDTO ToDTO(this UpdateUserRequest request)
        {
            return new UserDTO
            {
                ID = request.ID,
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
            };
        }

        public static UserResponse ToResponse(this UserDTO dto)
        {
            return new UserResponse
            {
                ID = dto.ID,
                Username = dto.Username,
                Email = dto.Email,
                Roles = dto.Roles,
                CreatedDate = dto.CreatedDate,
                LastActiveDate = dto.LastActiveDate,
                IsActive = dto.IsActive,
                IsDeleted = dto.IsDeleted,
            };
        }

        public static UserDTO ToDTO(this UserResponse response)
        {
            return new UserDTO
            {
                ID = response.ID,
                Username = response.Username,
                Email = response.Email,
                Roles = response.Roles,
                CreatedDate = response.CreatedDate,
                LastActiveDate = response.LastActiveDate,
                IsActive = response.IsActive,
                IsDeleted = response.IsDeleted,
            };
        }
    }
}
