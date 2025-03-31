using Vibora_API.Contracts.Request.Comment;
using Vibora_API.Contracts.Response.Comment;
using Vibora_API.Models.DTO;

namespace Vibora_API.Mappers
{
    public static class CommentMapper
    {
        public static CommentDTO ToDTO(this CreateCommentRequest request)
        {
            return new CommentDTO
            {
                ID = Guid.NewGuid(),
                UserID = request.UserID,
                PostID = request.PostID,
                Content = request.Content
            };
        }

        public static CommentDTO ToDTO(this UpdateCommentRequest request)
        {
            return new CommentDTO
            {
                ID = request.ID,
                UserID = request.UserID,
                PostID = request.PostID,
                Content = request.Content
            };
        }

        public static CommentResponse ToResponse(this CommentDTO response)
        {
            return new CommentResponse
            {
                ID = response.ID,
                UserID = response.UserID,
                PostID = response.PostID,
                Content = response.Content,
                CreatedDate = response.CreatedDate,
                IsHidden = response.IsHidden,
                IsDeleted = response.IsDeleted
            };
        }

        public static CommentDTO ToDTO(this CommentResponse response)
        {
            return new CommentDTO
            {
                ID = response.ID,
                UserID = response.UserID,
                PostID = response.PostID,
                Content = response.Content,
                CreatedDate = response.CreatedDate,
                IsHidden = response.IsHidden,
                IsDeleted = response.IsDeleted
            };
        }
    }
}
