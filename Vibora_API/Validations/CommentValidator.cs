using FluentValidation;
using Vibora_API.Models.DTO;

namespace Vibora_API.Validations
{
    public class CommentValidator : AbstractValidator<CommentDTO>
    {
        public CommentValidator()
        {
            var requiredMsg = "{PropertyName} is required";
            var lengthMsg = "{PropertyName} cannot exceed 100 characters";

            RuleFor(p => p.Content)
                .NotEmpty().WithMessage(requiredMsg)
                .MaximumLength(500).WithMessage(lengthMsg);
        }
    }
}
