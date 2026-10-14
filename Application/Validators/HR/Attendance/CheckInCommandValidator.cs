using Application.Commands.HR.Attendance;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.HR.Attendance
{
    public class CheckInCommandValidator : AbstractValidator<CheckInCommand>
    {
        public CheckInCommandValidator()
        {
            RuleFor(x => x.Request.Token)
                .NotEmpty()
                .WithMessage("QR Token is required.");
        }
    }
}
