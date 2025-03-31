using Vibora_API.Models.DTO;

namespace Vibora_API.Services
{
    public interface IUsersService
    {
        Task<UserDTO> AddUserAsync(UserDTO userDTO);
        Task<bool> DeleteUserAsync(Guid id);
        Task<UserDTO?> GetUserByEmailAsync(string email);
        Task<UserDTO?> GetUserByIdAsync(Guid id);
        Task<HashSet<string>> GetUserPermissionsAsync(Guid id);
        Task<HashSet<string>> GetUserRolesAsync(Guid id);
        Task<IEnumerable<UserDTO>?> GetUsersAsync();
        Task<UserDTO?> UpdateUserAsync(Guid id, UserDTO userDTO);
    }
}