using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vibora_API.Auth;
using Vibora_API.Contracts.Request.Thread;
using Vibora_API.Mappers;
using Vibora_API.Models.DTO;
using Vibora_API.Models.Enums;
using Vibora_API.Services;

namespace Vibora_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadsController(
        IThreadsService threadsService,
        IValidator<ThreadDTO> validator) : ControllerBase
    {
        private readonly IThreadsService _threadsService = threadsService;
        private readonly IValidator<ThreadDTO> _validator = validator;
        [HasPermissionAtribute(PermissionEnum.ThreadCreate)]
        [HttpPost]
        public async Task<IActionResult> CreateThread([FromBody] CreateThreadRequest request)
        {
            var thread = request.ToDTO();
            var validationResult = _validator.Validate(thread);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var createdThread = await _threadsService.AddThreadAsync(thread);
            var response = createdThread.ToResponse();
            return CreatedAtAction(nameof(GetThread), new { id = createdThread.ID }, response);
        }
        [HasPermissionAtribute(PermissionEnum.ThreadRead)]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetThread([FromRoute] Guid id)
        {
            var thread = await _threadsService.GetThreadByIdAsync(id);
            if (thread == null) return NotFound();
            var response = thread.ToResponse();
            return Ok(response);
        }
        [HasPermissionAtribute(PermissionEnum.ThreadRead)]
        [HttpGet]
        public async Task<IActionResult> GetThreads()
        {
            var threads = await _threadsService.GetThreadsAsync();
            return Ok(threads);
        }
        [HasPermissionAtribute(PermissionEnum.ThreadUpdate)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateThread([FromRoute] Guid id, UpdateThreadRequest request)
        {
            var thread = request.ToDTO();
            var validationResult = _validator.Validate(thread);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var updatedThread = await _threadsService.UpdateThreadAsync(id, thread);
            if (updatedThread == null) return NotFound();
            var resonse = updatedThread.ToResponse();
            return Ok(updatedThread);
        }
        [HasPermissionAtribute(PermissionEnum.ThreadDelete)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteThread([FromRoute] Guid id)
        {
            var deleted = await _threadsService.DeleteThreadAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
