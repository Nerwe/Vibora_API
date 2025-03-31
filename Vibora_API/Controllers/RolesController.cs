using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vibora_API.Auth;
using Vibora_API.Contracts.Request.Role;
using Vibora_API.Mappers;
using Vibora_API.Models.DTO;
using Vibora_API.Models.Enums;
using Vibora_API.Services;

namespace Vibora_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(
        IRolesService rolesService,
        IValidator<RoleDTO> validator) : ControllerBase
    {
        private readonly IRolesService _rolesService = rolesService;
        private readonly IValidator<RoleDTO> _validator = validator;

        [HasPermissionAtribute(PermissionEnum.RoleCreate)]
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleRequest request)
        {
            var roleDTO = request.ToDTO();
            var validationResult = _validator.Validate(roleDTO);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var createdRole = await _rolesService.AddRoleAsync(roleDTO);
            var response = createdRole.ToResponse();
            return CreatedAtAction(nameof(GetRole), new { id = createdRole.ID }, response);
        }

        [HasPermissionAtribute(PermissionEnum.RoleRead)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRole([FromRoute] int id)
        {
            var role = await _rolesService.GetRoleByIdAsync(id);
            if (role == null) return NotFound();
            var response = role.ToResponse();
            return Ok(response);
        }

        [HasPermissionAtribute(PermissionEnum.RoleRead)]
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _rolesService.GetRolesAsync();
            return Ok(roles);
        }

        [HasPermissionAtribute(PermissionEnum.RoleUpdate)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRole([FromRoute] int id, UpdateRoleRequest role)
        {
            var roleDTO = role.ToDTO();
            var validationResult = _validator.Validate(roleDTO);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var updatedRole = await _rolesService.UpdateRoleAsync(id, roleDTO);
            if (updatedRole == null) return NotFound();
            var response = updatedRole.ToResponse();
            return Ok(response);
        }

        [HasPermissionAtribute(PermissionEnum.RoleDelete)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            var deleted = await _rolesService.DeleteRoleAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
