using FluentValidation;
using Vibora_API.Models.DTO;

namespace Vibora_API.Validations
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            var requiredMsg = "{PropertyName} is required";
            var lengthMsg = "{PropertyName} cannot exceed 100 characters";
            var formatMsg = "Invalid {PropertyName} format";

            RuleFor(u => u.Username)
                .NotEmpty().WithMessage(requiredMsg)
                .MaximumLength(100).WithMessage(lengthMsg);

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage(requiredMsg)
                .EmailAddress().WithMessage(formatMsg);

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage(requiredMsg)
                .MaximumLength(100).WithMessage(lengthMsg);
        }
    }
}
