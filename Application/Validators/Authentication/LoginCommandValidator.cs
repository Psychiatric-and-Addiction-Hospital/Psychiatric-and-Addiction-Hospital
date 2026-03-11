using Application.Commands.Authentication;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Authentication
{
    public class LoginCommandValidator: AbstractValidator<LoginCommand>
    {

        public LoginCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not a valid email address");

            RuleFor(x => x.Password)
    .NotEmpty().WithMessage("Password is required")
    .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
    .WithMessage("Password must contain: lowercase, uppercase, number, special character and be at least 8 characters long");

        }
    }
}
