using Vibora_API.Models.DTO;

namespace Vibora_API.Auth
{
    public interface IJwtProvider
    {
        Task<string> GenerateToken(UserDTO userDTO);
    }
}