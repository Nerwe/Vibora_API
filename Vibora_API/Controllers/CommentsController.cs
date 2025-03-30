using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vibora_API.Models.DTO;
using Vibora_API.Services;

namespace Vibora_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController(
        ICommentsService commentsService,
        IValidator<CommentDTO> validator) : ControllerBase
    {
        private readonly ICommentsService _commentsService = commentsService;
        private readonly IValidator<CommentDTO> _validator = validator;
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentDTO comment)
        {
            var validationResult = _validator.Validate(comment);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var createdComment = await _commentsService.AddCommentAsync(comment);
            return CreatedAtAction(nameof(GetComment), new { id = createdComment.ID }, createdComment);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetComment([FromRoute] Guid id)
        {
            var comment = await _commentsService.GetCommentByIdAsync(id);
            if (comment == null) return NotFound();
            return Ok(comment);
        }
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentsService.GetCommentsAsync();
            return Ok(comments);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateComment([FromRoute] Guid id, CommentDTO comment)
        {
            var validationResult = _validator.Validate(comment);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var updatedComment = await _commentsService.UpdateCommentAsync(id, comment);
            if (updatedComment == null) return NotFound();
            return Ok(updatedComment);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
            var deleted = await _commentsService.DeleteCommentAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
