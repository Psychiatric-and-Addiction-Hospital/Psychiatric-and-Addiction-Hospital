using Application.Commands.Patient;
using FluentValidation;

namespace Application.Validators.Patient
{
    public class CreatePublicBookingCommandValidator : AbstractValidator<CreatePublicBookingCommand>
    {
        public CreatePublicBookingCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MinimumLength(3).WithMessage("Full name must be at least 3 characters.")
                .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .MinimumLength(10).WithMessage("Phone number must be at least 10 digits.")
                .MaximumLength(15).WithMessage("Phone number cannot exceed 15 digits.");


            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address format.");

            RuleFor(x => x.DoctorId)
                .NotEmpty().WithMessage("DoctorId is required.");

            RuleFor(x => x.ScheduleId)
                .NotEmpty().WithMessage("ScheduleId is required.");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Notes));
        }
    }
}
