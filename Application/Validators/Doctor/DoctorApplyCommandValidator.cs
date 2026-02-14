using Application.Commands.Doctores;
using FluentValidation;


namespace Application.Validators.Doctor
{
    public class DoctorApplyCommandValidator : AbstractValidator<DoctorApplyCommand>
    {
        public DoctorApplyCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MaximumLength(100).WithMessage("Full Name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+?[0-9]\d{1,14}$").WithMessage("PhoneNumber is not a valid phone number");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid gender value.");

            RuleFor(x => x.Specialization)
                .NotEmpty().WithMessage("Specialization is required.")
                .MaximumLength(100);

            RuleFor(x => x.Qualifications)
                .NotEmpty().WithMessage("Qualifications are required.")
                .MaximumLength(500);

            RuleFor(x => x.LicenseNumber)
                .NotEmpty().WithMessage("License Number is required.")
                .MaximumLength(50);

            RuleFor(x => x.ClinicAddress)
                .NotEmpty().WithMessage("Clinic Address is required.")
                .MaximumLength(200);

            RuleFor(x => x.NationalId)
                .NotEmpty().WithMessage("National ID is required.")
                .Matches(@"^[0-9]{14}$").WithMessage("National ID must be 14 digits.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200);

            RuleFor(x => x.Degree)
                .NotEmpty().WithMessage("Degree is required.")
                .MaximumLength(100);
        }

    }
}
