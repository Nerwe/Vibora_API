using Vibora_API.Contracts.Request.Auth;
using Vibora_API.Models.DTO;

namespace Vibora_API.Mappers
{
    public static class RegisterMapper
    {
        public static UserDTO ToDTO(this RegisterRequest request)
        {
            return new UserDTO
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            };
        }
    }
}
