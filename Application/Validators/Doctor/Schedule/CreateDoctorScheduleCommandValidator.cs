using Application.Commands.Doctores.Schedule;
using FluentValidation;
using System;

namespace Application.Validators.Doctor.Schedule
{
    public class CreateDoctorScheduleCommandValidator : AbstractValidator<CreateDoctorScheduleCommand>
    {
        public CreateDoctorScheduleCommandValidator()
        {
            RuleFor(x => x.request.Date)
                .NotEmpty().WithMessage("Date is required.")
                .Must(BeFutureDate)
                .WithMessage("Date must be in the future.");

            RuleFor(x => x.request.Time)
                .NotEmpty().WithMessage("Time is required.")
                .Must(BeValidTime)
                .WithMessage("Time is invalid.");
        }

        private bool BeFutureDate(DateOnly date)
        {
            return date >= DateOnly.FromDateTime(DateTime.UtcNow);
        }
        private bool BeValidTime(TimeOnly time)
        {
            return time.Hour >= 0 && time.Hour <= 23;
        }

    }
}
