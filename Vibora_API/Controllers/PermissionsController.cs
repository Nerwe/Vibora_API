using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vibora_API.Auth;
using Vibora_API.Contracts.Request.Permission;
using Vibora_API.Mappers;
using Vibora_API.Models.DTO;
using Vibora_API.Models.Enums;
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
        [HasPermissionAtribute(PermissionEnum.PermissionCreate)]
        [HttpPost]
        public async Task<IActionResult> CreatePermission([FromBody] CreatePermissionRequest request)
        {
            var permission = request.ToDTO();
            var validationResult = _validator.Validate(permission);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var createdPermission = await _permissionsService.AddPermissionAsync(permission);
            var response = createdPermission.ToResponse();
            return CreatedAtAction(nameof(GetPermission), new { id = createdPermission.ID }, response);
        }
        [HasPermissionAtribute(PermissionEnum.PermissionRead)]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPermission([FromRoute] int id)
        {
            var permission = await _permissionsService.GetPermissionByIdAsync(id);
            if (permission == null) return NotFound();
            var response = permission.ToResponse();
            return Ok(response);
        }
        [HasPermissionAtribute(PermissionEnum.PermissionRead)]
        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await _permissionsService.GetPermissionsAsync();
            return Ok(permissions);
        }
        [HasPermissionAtribute(PermissionEnum.PermissionUpdate)]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePermission([FromRoute] int id, UpdatePermissionRequest request)
        {
            var permission = request.ToDTO();
            var validationResult = _validator.Validate(permission);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var updatedPermission = await _permissionsService.UpdatePermissionAsync(id, permission);
            if (updatedPermission == null) return NotFound();
            var response = updatedPermission.ToResponse();
            return Ok(response);
        }
        [HasPermissionAtribute(PermissionEnum.PermissionDelete)]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePermission([FromRoute] int id)
        {
            var deleted = await _permissionsService.DeletePermissionAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
