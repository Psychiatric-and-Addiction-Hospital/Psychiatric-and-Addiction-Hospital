using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validator_DTO
{
    public class ResetPasswordOtpDtoValidator : AbstractValidator<ResetPasswordOtpDto>
    {
        public ResetPasswordOtpDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not EmailAddress");
            RuleFor(x => x.Otp)
                .NotEmpty().WithMessage("OTP is required");
            RuleFor(x => x.NewPassword)
               .NotEmpty().WithMessage("Password is required")
               .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
               .WithMessage("Password must be at least 8 characters long and contain an uppercase letter, lowercase letter, number, and special character");
            RuleFor(x => x.ConfirmPassword)
               .Equal(x => x.NewPassword).WithMessage("Passwords do not match");
        }
    }
}
