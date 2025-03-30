using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vibora_API.Contracts.Request.Role;
using Vibora_API.Mappers;
using Vibora_API.Models.DTO;
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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRole([FromRoute] int id)
        {
            var role = await _rolesService.GetRoleByIdAsync(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _rolesService.GetRolesAsync();
            return Ok(roles);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateRole([FromRoute] int id, RoleDTO role)
        {
            var validationResult = _validator.Validate(role);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var updatedRole = await _rolesService.UpdateRoleAsync(id, role);
            if (updatedRole == null) return NotFound();
            return Ok(updatedRole);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            var deleted = await _rolesService.DeleteRoleAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
