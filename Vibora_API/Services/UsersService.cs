using Vibora_API.Models.DTO;
using Vibora_API.Repositories;

namespace Vibora_API.Services
{
    public class UsersService(IUsersRepository usersRepository) : IUsersService
    {
        private readonly IUsersRepository _usersRepository = usersRepository;
        public async Task<IEnumerable<UserDTO>?> GetUsersAsync()
        {
            return await _usersRepository.GetAsync();
        }
        public async Task<UserDTO?> GetUserByIdAsync(Guid id)
        {
            return await _usersRepository.GetByIdAsync(id);
        }
        public async Task<UserDTO> AddUserAsync(UserDTO userDTO)
        {
            return await _usersRepository.AddAsync(userDTO);
        }
        public async Task<UserDTO?> UpdateUserAsync(Guid id, UserDTO userDTO)
        {
            return await _usersRepository.UpdateAsync(id, userDTO);
        }
        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await _usersRepository.DeleteAsync(id);
        }
    }
}
