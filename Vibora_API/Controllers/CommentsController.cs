using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vibora_API.Auth;
using Vibora_API.Contracts.Request.Comment;
using Vibora_API.Mappers;
using Vibora_API.Models.DTO;
using Vibora_API.Models.Enums;
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

        [HasPermissionAtribute(PermissionEnum.CommentCreate)]
        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request)
        {
            var comment = request.ToDTO();
            var validationResult = _validator.Validate(comment);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var createdComment = await _commentsService.AddCommentAsync(comment);
            var response = createdComment.ToResponse();
            return CreatedAtAction(nameof(GetComment), new { id = createdComment.ID }, response);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetComment([FromRoute] Guid id)
        {
            var comment = await _commentsService.GetCommentByIdAsync(id);
            if (comment == null) return NotFound();
            var response = comment.ToResponse();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentsService.GetCommentsAsync();
            if (comments == null) return NotFound();
            var response = comments.Select(c => c.ToResponse());
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("post/{postId:guid}")]
        public async Task<IActionResult> GetCommentsByPost([FromRoute] Guid postId)
        {
            var comments = await _commentsService.GetCommentByPostIdAsync(postId);
            if (comments == null) return NotFound();
            var response = comments.Select(c => c.ToResponse());
            return Ok(response);
        }

        [HasPermissionAtribute(PermissionEnum.CommentUpdate)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateComment([FromRoute] Guid id, UpdateCommentRequest request)
        {
            var comment = request.ToDTO();
            var validationResult = _validator.Validate(comment);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var updatedComment = await _commentsService.UpdateCommentAsync(id, comment);
            if (updatedComment == null) return NotFound();
            var response = updatedComment.ToResponse();
            return Ok(response);
        }

        [HasPermissionAtribute(PermissionEnum.CommentDelete)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteComment([FromRoute] Guid id)
        {
            var deleted = await _commentsService.DeleteCommentAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
