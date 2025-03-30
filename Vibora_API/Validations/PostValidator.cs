using FluentValidation;
using Vibora_API.Models.DTO;

namespace Vibora_API.Validations
{
    public class PostValidator : AbstractValidator<PostDTO>
    {
        public PostValidator()
        {
            var requiredMsg = "{PropertyName} is required";
            var lengthMsg = "{PropertyName} cannot exceed 100 characters";

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage(requiredMsg)
                .MaximumLength(100).WithMessage(lengthMsg);

            RuleFor(p => p.Content)
                .NotEmpty().WithMessage(requiredMsg)
                .MaximumLength(5000).WithMessage(lengthMsg);
        }
    }
}
