using Application.Commands.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Authentication
{
    public class RegisterCommandValidator: AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required")
                .MinimumLength(3).WithMessage("FirstName must be at least 3 characters")
                .MaximumLength(20).WithMessage("FirstName must not exceed 20 characters");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required")
                .MinimumLength(3).WithMessage("FirstName must be at least 3 characters")
                .MaximumLength(20).WithMessage("LastName must not exceed 20 characters");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not a valid email address");
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("PhoneNumber is required")
                .Matches(@"^\+?[0-9]\d{1,14}$").WithMessage("PhoneNumber is not a valid phone number");
            RuleFor(x => x.Addres)
                .NotEmpty().WithMessage("Addres is required")
                .MaximumLength(200).WithMessage("Addres must not exceed 200 characters");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");
            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("ConfirmPassword must match Password");
        }
    }
}
