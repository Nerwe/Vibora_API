using Vibora_API.Contracts.Request.Thread;
using Vibora_API.Contracts.Response.Thread;
using Vibora_API.Models.DTO;

namespace Vibora_API.Mappers
{
    public static class ThreadMapper
    {
        public static ThreadDTO ToDTO(this CreateThreadRequest request)
        {
            return new ThreadDTO
            {
                ID = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                UserID = request.UserID
            };
        }
        public static ThreadDTO ToDTO(this UpdateThreadRequest request)
        {
            return new ThreadDTO
            {
                ID = request.ID,
                Title = request.Title,
                Description = request.Description,
                UserID = request.UserID
            };
        }
        public static ThreadResponse ToResponse(this ThreadDTO dto)
        {
            return new ThreadResponse
            {
                ID = dto.ID,
                Title = dto.Title,
                Description = dto.Description,
                IsDeleted = dto.IsDeleted
            };
        }
        public static ThreadDTO ToDTO(this ThreadResponse response)
        {
            return new ThreadDTO
            {
                ID = response.ID,
                Title = response.Title,
                Description = response.Description,
                IsDeleted = response.IsDeleted
            };
        }
    }
}
