using Application.Commands.Doctores;
using FluentValidation;
using System;
using System.Linq;


namespace Application.Validators.Doctor
{
    public class DoctorApplyCommandValidator : AbstractValidator<DoctorApplyCommand>
    {
        public DoctorApplyCommandValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MinimumLength(3).WithMessage("Full name must be at least 3 characters.")
                .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches("^[0-9]+$").WithMessage("Phone number must contain only digits.")
                .MinimumLength(10).WithMessage("Phone number must be at least 10 digits.")
                .MaximumLength(15).WithMessage("Phone number cannot exceed 15 digits.");

            RuleFor(x => x.Gender)
                .IsInEnum().WithMessage("Invalid gender value.");

            RuleFor(x => x.Specialization)
                .NotEmpty().WithMessage("Specialization is required.")
                .MinimumLength(3).WithMessage("Specialization must be at least 3 characters.")
                .MaximumLength(200).WithMessage("Specialization cannot exceed 200 characters.");

            RuleFor(x => x.Qualifications)
                .NotEmpty().WithMessage("Qualifications are required.")
                .MinimumLength(3).WithMessage("Qualifications must be at least 3 characters.")
                .MaximumLength(200).WithMessage("Qualifications cannot exceed 200 characters.");

            RuleFor(x => x.LicenseNumber)
                .NotEmpty().WithMessage("License number is required.")
                .MinimumLength(5).WithMessage("License number must be at least 5 characters.")
                .MaximumLength(20).WithMessage("License number cannot exceed 20 characters.");

            RuleFor(x => x.Experience)
                .NotEmpty().WithMessage("Experience is required.")
                .MinimumLength(3).WithMessage("Experience must be at least 3 characters.")
                .MaximumLength(200).WithMessage("Experience cannot exceed 200 characters.");

            RuleFor(x => x.NationalId)
                .NotEmpty().WithMessage("National ID is required.")
                .Matches("^[0-9]{14}$").WithMessage("National ID must be exactly 14 digits.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MinimumLength(5).WithMessage("Address must be at least 5 characters.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");

            RuleFor(x => x.Degree)
                .NotEmpty().WithMessage("Degree is required.")
                .MinimumLength(2).WithMessage("Degree must be at least 2 characters.")
                .MaximumLength(50).WithMessage("Degree cannot exceed 50 characters.");

            When(x => x.ProfileImage != null, () =>
            {
                RuleFor(x => x.ProfileImage.Length)
                    .LessThanOrEqualTo(2 * 1024 * 1024) // 2 MB
                    .WithMessage("Profile image size must be less than or equal to 2MB.");

                RuleFor(x => x.ProfileImage.FileName)
                    .Must(IsValidImageExtension)
                    .WithMessage("Profile image must be a valid format (jpg, jpeg, png).");
            });
        }

        private bool IsValidImageExtension(string fileName)
        {
            var extensions = new[] { ".jpg", ".jpeg", ".png" };
            return extensions.Any(ext => fileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }

    }
}