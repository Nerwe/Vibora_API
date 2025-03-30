using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vibora_API.Models.DTO;
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
        [HttpPost]
        public async Task<IActionResult> CreateThread([FromBody] ThreadDTO thread)
        {
            var validationResult = _validator.Validate(thread);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var createdThread = await _threadsService.AddThreadAsync(thread);
            return CreatedAtAction(nameof(GetThread), new { id = createdThread.ID }, createdThread);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetThread([FromRoute] Guid id)
        {
            var thread = await _threadsService.GetThreadByIdAsync(id);
            if (thread == null) return NotFound();
            return Ok(thread);
        }
        [HttpGet]
        public async Task<IActionResult> GetThreads()
        {
            var threads = await _threadsService.GetThreadsAsync();
            return Ok(threads);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateThread([FromRoute] Guid id, ThreadDTO thread)
        {
            var validationResult = _validator.Validate(thread);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var updatedThread = await _threadsService.UpdateThreadAsync(id, thread);
            if (updatedThread == null) return NotFound();
            return Ok(updatedThread);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteThread([FromRoute] Guid id)
        {
            var deleted = await _threadsService.DeleteThreadAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
