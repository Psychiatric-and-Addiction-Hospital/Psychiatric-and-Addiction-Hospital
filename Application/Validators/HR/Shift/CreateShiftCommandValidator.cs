using Application.Commands.HR.Shift;
using FluentValidation;

namespace Application.Validators.HR.Shift
{
    public class CreateShiftCommandValidator:AbstractValidator<CreateShiftCommand>
    {
        public CreateShiftCommandValidator()
        {
            RuleFor(x => x.request.Name)
                .NotEmpty()
                .WithMessage("Shift name is required.")
                .MaximumLength(100)
                .WithMessage("Shift name must not exceed 100 characters.");

            RuleFor(x => x.request.StartTime)
                .NotEmpty()
                .WithMessage("Start time is required.");

            RuleFor(x => x.request.EndTime)
                .NotEqual(x => x.request.StartTime)
                .WithMessage("End time must be different from start time.");

            RuleFor(x => x.request.BreakMinutes)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Break minutes cannot be negative.");

            RuleFor(x => x.request.ToleranceMinutes)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Tolerance minutes cannot be negative.");
        }
    }
}
