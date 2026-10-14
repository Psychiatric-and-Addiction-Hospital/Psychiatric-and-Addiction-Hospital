using Application.Commands.Authentication;
using FluentValidation;

namespace Application.Validators.Authentication
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            RuleFor(x => x.request.FirstName)
                .NotEmpty().WithMessage("FirstName is required")
                .MinimumLength(3).WithMessage("FirstName must be at least 3 characters")
                .MaximumLength(20).WithMessage("FirstName must not exceed 20 characters");
            RuleFor(x => x.request.LastName)
                .NotEmpty().WithMessage("LastName is required")
                .MinimumLength(3).WithMessage("FirstName must be at least 3 characters")
                .MaximumLength(20).WithMessage("LastName must not exceed 20 characters");
            RuleFor(x => x.request.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not a valid email address");
            RuleFor(x => x.request.Gender)
               .IsInEnum().WithMessage("Invalid gender value.");
            RuleFor(x => x.request.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required")
                .Matches(@"^\+?[0-9]\d{1,14}$").WithMessage("PhoneNumber is not a valid phone number");
            RuleFor(x => x.request.Address)
                .NotEmpty().WithMessage("Addres is required")
                .MaximumLength(200).WithMessage("Addres must not exceed 200 characters");
            RuleFor(x => x.request.Password)
    .NotEmpty().WithMessage("Password is required")
    .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
    .WithMessage("Password must contain: lowercase, uppercase, number, special character and be at least 8 characters long");

            RuleFor(x => x.request.ConfirmPassword)
                .Equal(x => x.request.Password).WithMessage("ConfirmPassword must match Password");
        }
    }
}
