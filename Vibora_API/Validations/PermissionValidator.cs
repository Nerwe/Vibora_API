using FluentValidation;
using Vibora_API.Models.DTO;

namespace Vibora_API.Validations
{
    public class PermissionValidator : AbstractValidator<PermissionDTO>
    {
        public PermissionValidator()
        {
            var requiredMsg = "{PropertyName} is required";
            var lengthMsg = "{PropertyName} cannot exceed 100 characters";

            RuleFor(p => p.Title)
                    .NotEmpty().WithMessage(requiredMsg)
                    .MaximumLength(100).WithMessage(lengthMsg);
        }
    }
}
