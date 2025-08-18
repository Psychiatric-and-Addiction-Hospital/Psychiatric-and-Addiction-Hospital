using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validator_DTO
{
    public class VerifyOtpDtovalidator:AbstractValidator<VerifyOtpDto>
    {
        public VerifyOtpDtovalidator()
        {
            RuleFor(x => x.Email)
           .NotEmpty().WithMessage("Email is required")
           .EmailAddress().WithMessage("Email is not EmailAddress");
            RuleFor(x => x.Code)
               .NotEmpty().WithMessage("code is required");
        }
    }
}
