using Application.Commands.Doctores.Schedule;
using FluentValidation;
using System;

namespace Application.Validators.Doctor.Schedule
{
    public class CreateDoctorScheduleCommandValidator : AbstractValidator<CreateDoctorScheduleCommand>
    {
        public CreateDoctorScheduleCommandValidator()
        {
            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("DoctorId is required.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Date is required.")
                .Must(BeFutureDate)
                .WithMessage("Date must be in the future.");

            RuleFor(x => x.Time)
                .NotEmpty().WithMessage("Time is required.")
                .Must(BeValidTime)
                .WithMessage("Time must be in HH:mm format (e.g., 14:30).");
        }

        private bool BeFutureDate(DateTime date)
        {
            return date.Date >= DateTime.UtcNow.Date;
        }

        private bool BeValidTime(string time)
        {
            return TimeSpan.TryParse(time, out _);
        }

    }
}
