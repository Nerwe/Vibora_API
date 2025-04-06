using Vibora_API.Contracts.Request.Post;
using Vibora_API.Contracts.Response.Post;
using Vibora_API.Models.DTO;

namespace Vibora_API.Mappers
{
    public static class PostMapper
    {
        public static PostDTO ToDTO(this CreatePostRequest request)
        {
            return new PostDTO
            {
                ID = Guid.NewGuid(),
                UserID = request.UserID,
                ThreadID = request.ThreadID,
                Title = request.Title,
                Content = request.Content,
            };
        }
        public static PostDTO ToDTO(this UpdatePostRequest request)
        {
            return new PostDTO
            {
                ID = request.ID,
                Title = request.Title,
                Content = request.Content,
                Score = request.Score,
                IsHidden = request.IsHidden,
                IsDeleted = request.IsDeleted,
            };
        }
        public static PostResponse ToResponse(this PostDTO dto)
        {
            return new PostResponse
            {
                ID = dto.ID,
                UserID = dto.UserID,
                ThreadID = dto.ThreadID,
                Title = dto.Title,
                Content = dto.Content,
                CreatedDate = dto.CreatedDate,
                LastUpdatedDate = dto.LastUpdatedDate,
                Score = dto.Score,
                IsHidden = dto.IsHidden,
                IsDeleted = dto.IsDeleted,
            };
        }
        public static PostDTO ToDTO(this PostResponse response)
        {
            return new PostDTO
            {
                ID = response.ID,
                UserID = response.UserID,
                ThreadID = response.ThreadID,
                Title = response.Title,
                Content = response.Content,
                CreatedDate = response.CreatedDate,
                LastUpdatedDate = response.LastUpdatedDate,
                Score = response.Score,
                IsHidden = response.IsHidden,
                IsDeleted = response.IsDeleted,
            };
        }
    }
}
