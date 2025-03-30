using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vibora_API.Contracts.Request.User;
using Vibora_API.Mappers;
using Vibora_API.Models.DTO;
using Vibora_API.Services;

namespace Vibora_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(
        IUsersService usersService,
        IValidator<UserDTO> validator) : ControllerBase
    {
        private readonly IUsersService _usersService = usersService;
        private readonly IValidator<UserDTO> _validator = validator;

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var userDTO = request.ToDTO();
            var validationResult = _validator.Validate(userDTO);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var createdUserDTO = await _usersService.AddUserAsync(userDTO);
            var response = createdUserDTO.ToResponse();
            return CreatedAtAction(nameof(GetUser), new { id = createdUserDTO.ID }, response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await _usersService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _usersService.GetUsersAsync();
            return Ok(users);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, UserDTO user)
        {
            var validationResult = _validator.Validate(user);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var updatedUser = await _usersService.UpdateUserAsync(id, user);
            if (updatedUser == null) return NotFound();
            return Ok(updatedUser);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var deleted = await _usersService.DeleteUserAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
