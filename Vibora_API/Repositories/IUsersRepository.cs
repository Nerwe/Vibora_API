using Vibora_API.Models.DTO;

namespace Vibora_API.Repositories
{
    public interface IUsersRepository
    {
        Task<UserDTO> AddAsync(UserDTO userDTO);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<UserDTO>> GetAsync();
        Task<UserDTO?> GetByIdAsync(Guid id);
        Task<UserDTO?> UpdateAsync(Guid id, UserDTO userDTO);
    }
}