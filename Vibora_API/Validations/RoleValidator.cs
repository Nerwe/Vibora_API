using FluentValidation;
using Vibora_API.Models.DTO;

namespace Vibora_API.Validations
{
    public class RoleValidator : AbstractValidator<RoleDTO>
    {
        public RoleValidator()
        {
            var requiredMsg = "{PropertyName} is required";
            var lengthMsg = "{PropertyName} cannot exceed 100 characters";

            RuleFor(u => u.Title)
                .NotEmpty().WithMessage(requiredMsg)
                .MaximumLength(100).WithMessage(lengthMsg);
        }
    }
}
