using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validator_DTO
{
    public class RegisterDtoValidator: AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator() 
        {
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName is required")
            .MinimumLength(4).WithMessage("FirstName must be at least 4 characters")
            .MaximumLength(15).WithMessage("FirstName must not exceed 15 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required")
                .MinimumLength(4).WithMessage("LastName must be at least 4 characters")
                .MaximumLength(15).WithMessage("LastName must not exceed 15 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not EmailAddress");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Invalid phone number");

            RuleFor(x => x.Addres)
                .NotEmpty().WithMessage("Address is required")
                .Length(5, 200).WithMessage("Address must be between 5 and 200 characters")
                .Matches(@"^[\u0600-\u06FFa-zA-Z0-9\s,.-]{5,200}$").WithMessage("Invalid address format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                .WithMessage("Password must be at least 8 characters long and contain an uppercase letter, lowercase letter, number, and special character");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("ConfirmPassword is required")
                .Equal(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}
