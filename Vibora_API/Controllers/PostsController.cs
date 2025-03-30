using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Vibora_API.Models.DTO;
using Vibora_API.Services;

namespace Vibora_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController(
        IPostsService postsService,
        IValidator<PostDTO> validator) : ControllerBase
    {
        private readonly IPostsService _postsService = postsService;
        private readonly IValidator<PostDTO> _validator = validator;
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostDTO post)
        {
            var validationResult = _validator.Validate(post);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var createdPost = await _postsService.AddPostAsync(post);
            return CreatedAtAction(nameof(GetPost), new { id = createdPost.ID }, createdPost);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPost([FromRoute] Guid id)
        {
            var post = await _postsService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postsService.GetPostsAsync();
            return Ok(posts);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid id, PostDTO post)
        {
            var validationResult = _validator.Validate(post);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var updatedPost = await _postsService.UpdatePostAsync(id, post);
            if (updatedPost == null) return NotFound();
            return Ok(updatedPost);
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePost([FromRoute] Guid id)
        {
            var deleted = await _postsService.DeletePostAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
