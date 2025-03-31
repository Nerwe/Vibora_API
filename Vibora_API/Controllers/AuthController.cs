using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vibora_API.Auth;
using Vibora_API.Contracts.Request.Auth;
using Vibora_API.Mappers;
using Vibora_API.Models.DTO;
using Vibora_API.Models.Enums;
using Vibora_API.Services;

namespace Vibora_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IPasswordHasher _passwordsService;
        private readonly IRolesService _rolesService;
        private readonly IJwtProvider _jwtProvider;
        private readonly IValidator<UserDTO> _validator;

        public AuthController(
            IUsersService usersService,
            IPasswordHasher passwordsService,
            IRolesService rolesService,
            IJwtProvider jwtProvider,
            IValidator<UserDTO> validator)
        {
            _usersService = usersService;
            _passwordsService = passwordsService;
            _rolesService = rolesService;
            _jwtProvider = jwtProvider;
            _validator = validator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var userDTO = request.ToDTO();

            var role1 = await _rolesService.GetRoleByIdAsync((int)RoleEnum.Admin);
            var role2 = await _rolesService.GetRoleByIdAsync((int)RoleEnum.Moderator);
            var role3 = await _rolesService.GetRoleByIdAsync((int)RoleEnum.User);

            if (role1 == null || role2 == null || role3 == null) return NotFound();

            userDTO.Roles = [role1, role2, role3];

            var validatorResult = await _validator.ValidateAsync(userDTO);
            validatorResult.AddToModelState(ModelState);

            if (!validatorResult.IsValid) return BadRequest(ModelState);

            var existingUser = await _usersService.GetUserByEmailAsync(userDTO.Email);
            if (existingUser != null) return Conflict();

            var passwordHash = _passwordsService.Generate(request.Password);
            userDTO.Password = passwordHash;

            var createdUserDTO = await _usersService.AddUserAsync(userDTO);
            var response = createdUserDTO.ToResponse();

            return Ok("User registered successfully");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var userDTO = request.ToDTO();

            var existingUser = await _usersService.GetUserByEmailAsync(userDTO.Email);
            if (existingUser == null) return NotFound();

            var result = _passwordsService.Verify(request.Password, existingUser.Password);
            if (result == false) return NotFound();

            var token = await _jwtProvider.GenerateToken(existingUser);

            Response.Cookies.Append("ulog", token);

            return Ok(token);
        }
    }
}
