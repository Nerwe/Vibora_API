using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vibora_API.Models.DTO;
using Vibora_API.Services;

namespace Vibora_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController(
        IPermissionsService permissionsService,
        IValidator<PermissionDTO> validator) : ControllerBase
    {
        private readonly IPermissionsService _permissionsService = permissionsService;
        private readonly IValidator<PermissionDTO> _validator = validator;
        [HttpPost]
        public async Task<IActionResult> CreatePermission([FromBody] PermissionDTO permission)
        {
            var validationResult = _validator.Validate(permission);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var createdPermission = await _permissionsService.AddPermissionAsync(permission);
            return CreatedAtAction(nameof(GetPermission), new { id = createdPermission.ID }, createdPermission);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPermission([FromRoute] int id)
        {
            var permission = await _permissionsService.GetPermissionByIdAsync(id);
            if (permission == null) return NotFound();
            return Ok(permission);
        }
        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await _permissionsService.GetPermissionsAsync();
            return Ok(permissions);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePermission([FromRoute] int id, PermissionDTO permission)
        {
            var validationResult = _validator.Validate(permission);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var updatedPermission = await _permissionsService.UpdatePermissionAsync(id, permission);
            if (updatedPermission == null) return NotFound();
            return Ok(updatedPermission);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePermission([FromRoute] int id)
        {
            var deleted = await _permissionsService.DeletePermissionAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
