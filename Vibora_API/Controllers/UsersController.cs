using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vibora_API.Auth;
using Vibora_API.Contracts.Request.User;
using Vibora_API.Mappers;
using Vibora_API.Models.DTO;
using Vibora_API.Models.Enums;
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

        [HasPermissionAtribute(PermissionEnum.UserCreate)]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var user = request.ToDTO();
            var validationResult = _validator.Validate(user);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var createdUser = await _usersService.AddUserAsync(user);
            var response = createdUser.ToResponse();
            return CreatedAtAction(nameof(GetUser), new { id = createdUser.ID }, response);
        }

        [HasPermissionAtribute(PermissionEnum.UserRead)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUser([FromRoute] Guid id)
        {
            var user = await _usersService.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            var response = user.ToResponse();
            return Ok(response);
        }

        [HasPermissionAtribute(PermissionEnum.UserRead)]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _usersService.GetUsersAsync();
            return Ok(users);
        }

        [HasPermissionAtribute(PermissionEnum.UserUpdate)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, UpdateUserRequest request)
        {
            var user = request.ToDTO();
            var validationResult = _validator.Validate(user);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var updatedUser = await _usersService.UpdateUserAsync(id, user);
            if (updatedUser == null) return NotFound();
            var response = updatedUser.ToResponse();
            return Ok(response);
        }

        [HasPermissionAtribute(PermissionEnum.UserDelete)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            var deleted = await _usersService.DeleteUserAsync(id);
            if (!deleted) return NotFound();
            return Ok("User was removed");
        }
    }
}
