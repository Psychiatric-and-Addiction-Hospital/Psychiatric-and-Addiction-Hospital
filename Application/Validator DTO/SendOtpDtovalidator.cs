using Application.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validator_DTO
{
    public class SendOtpDtovalidator:AbstractValidator<SendOtpDto>
    {
        public SendOtpDtovalidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Email is required")
               .EmailAddress().WithMessage("Email is not EmailAddress");
        }
    }
}
