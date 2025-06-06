﻿using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vibora_API.Auth;
using Vibora_API.Contracts.Request.Post;
using Vibora_API.Mappers;
using Vibora_API.Models.DTO;
using Vibora_API.Models.Enums;
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
        [HasPermissionAtribute(PermissionEnum.PostCreate)]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
        {
            var post = request.ToDTO();
            var validationResult = _validator.Validate(post);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var createdPost = await _postsService.AddPostAsync(post);
            var response = createdPost.ToResponse();
            return CreatedAtAction(nameof(GetPost), new { id = createdPost.ID }, response);
        }

        [AllowAnonymous]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPost([FromRoute] Guid id)
        {
            var post = await _postsService.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postsService.GetPostsAsync();
            if (posts == null) return NotFound();
            var response = posts.Select(p => p.ToResponse());
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("thread/{threadId:guid}")]
        public async Task<IActionResult> GetPostsByThread([FromRoute] Guid threadId)
        {
            var posts = await _postsService.GetPostsByThreadAsync(threadId);
            if (posts == null) return NotFound();
            var response = posts.Select(p => p.ToResponse());
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetPostsByUser([FromRoute] Guid userId)
        {
            var posts = await _postsService.GetPostsByUserIdAsync(userId);
            if (posts == null) return NotFound();
            var response = posts.Select(p => p.ToResponse());
            return Ok(response);
        }

        [HasPermissionAtribute(PermissionEnum.PostUpdate)]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePost([FromRoute] Guid id, UpdatePostRequest request)
        {
            var post = request.ToDTO();
            var validationResult = _validator.Validate(post);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var updatedPost = await _postsService.UpdatePostAsync(id, post);
            if (updatedPost == null) return NotFound();
            var response = updatedPost.ToResponse();
            return Ok(response);
        }

        [HasPermissionAtribute(PermissionEnum.PostDelete)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePost([FromRoute] Guid id)
        {
            var deleted = await _postsService.DeletePostAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
