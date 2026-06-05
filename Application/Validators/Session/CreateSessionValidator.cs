using Application.Commands.Session;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Session
{
    public class CreateSessionValidator : AbstractValidator<CreateSessionCommand>
    {
        public CreateSessionValidator()
        {
            RuleFor(x => x.DoctorId).NotEmpty();
            RuleFor(x => x.PatientId).NotEmpty();
            RuleFor(x => x.ScheduledDate).GreaterThan(DateTime.Now).WithMessage("Session date must be in the future!");
            RuleFor(x => x.DurationMinutes).InclusiveBetween(15, 120).WithMessage("Session duration must be between 15 to 120 minutes!");
        }
    }
}
