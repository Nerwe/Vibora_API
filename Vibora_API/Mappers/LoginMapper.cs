using Vibora_API.Contracts.Request.Auth;
using Vibora_API.Models.DTO;

namespace Vibora_API.Mappers
{
    public static class LoginMapper
    {
        public static UserDTO ToDTO(this LoginRequest request)
        {
            return new UserDTO
            {
                Email = request.Email,
                Password = request.Password
            };
        }
    }
}
