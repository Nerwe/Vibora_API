using Microsoft.EntityFrameworkCore;
using Vibora_API.Data;
using Vibora_API.Models.DB;
using Vibora_API.Models.DTO;

namespace Vibora_API.Repositories
{
    public class UsersRepository(ViboraDBContext context) : IUsersRepository
    {
        private readonly ViboraDBContext _context = context;

        public async Task<IEnumerable<UserDTO>> GetAsync()
        {
            var users = await _context.Users.ToListAsync();
            var userDTOs = users.Select(u => new UserDTO
            {
                ID = u.ID,
                Username = u.Username,
                Email = u.Email,
                Password = u.Password,
                CreatedDate = u.CreatedDate,
                LastActiveDate = u.LastActiveDate,
                Roles = u.Roles.Select(r => new RoleDTO
                {
                    ID = r.ID,
                    Title = r.Title
                }).ToList(),
                IsActive = u.IsActive,
                IsDeleted = u.IsDeleted
            });
            return userDTOs;
        }

        public async Task<UserDTO?> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
            if (user == null) return null;
            var userDTO = new UserDTO
            {
                ID = user.ID,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                CreatedDate = user.CreatedDate,
                LastActiveDate = user.LastActiveDate,
                Roles = user.Roles.Select(r => new RoleDTO
                {
                    ID = r.ID,
                    Title = r.Title
                }).ToList(),
                IsActive = user.IsActive,
                IsDeleted = user.IsDeleted
            };
            return userDTO;
        }

        public async Task<UserDTO> AddAsync(UserDTO userDTO)
        {
            var user = new User
            {
                ID = userDTO.ID,
                Username = userDTO.Username,
                Email = userDTO.Email,
                Password = userDTO.Password,
                CreatedDate = userDTO.CreatedDate,
                LastActiveDate = userDTO.LastActiveDate,
                IsActive = userDTO.IsActive,
                IsDeleted = userDTO.IsDeleted,
                Roles = userDTO.Roles.Select(r => new Role
                {
                    ID = r.ID,
                    Title = r.Title
                }).ToList()
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return userDTO;
        }

        public async Task<UserDTO?> UpdateAsync(Guid id, UserDTO userDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
            if (user == null) return null;
            user.Username = userDTO.Username;
            user.Email = userDTO.Email;
            user.CreatedDate = userDTO.CreatedDate;
            user.LastActiveDate = userDTO.LastActiveDate;
            user.IsActive = userDTO.IsActive;
            user.IsDeleted = userDTO.IsDeleted;
            user.Roles = userDTO.Roles.Select(r => new Role
            {
                ID = r.ID,
                Title = r.Title
            }).ToList();
            await _context.SaveChangesAsync();
            return userDTO;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ID == id);
            if (user == null) return false;
            user.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
