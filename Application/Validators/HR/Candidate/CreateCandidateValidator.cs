using Application.Commands.HR.Candidate;
using FluentValidation;

namespace Application.Validators.HR.Candidate
{
    public class CreateCandidateValidator : AbstractValidator<CreateCandidateCommand>
    {
        public CreateCandidateValidator()
        {

            RuleFor(x => x.Request.FirstName)
                        .NotEmpty()
                        .WithMessage("First name is required.")
                        .MaximumLength(100)
                        .WithMessage("First name cannot exceed 100 characters.");

            RuleFor(x => x.Request.LastName)
                .NotEmpty()
                .WithMessage("Last name is required.")
                .MaximumLength(100)
                .WithMessage("Last name cannot exceed 100 characters.");

            RuleFor(x => x.Request.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Please enter a valid email address.")
                .MaximumLength(256)
                .WithMessage("Email cannot exceed 256 characters.");

            RuleFor(x => x.Request.PhoneNumber)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .MaximumLength(20)
                .WithMessage("Phone number cannot exceed 20 characters.");

            RuleFor(x => x.Request.YearsOfExperience)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Years of experience cannot be negative.");

            RuleFor(x => x.Request.CurrentCompany)
                .MaximumLength(200)
                .WithMessage("Current company cannot exceed 200 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Request.CurrentCompany));

            RuleFor(x => x.Request.CurrentPosition)
                .MaximumLength(200)
                .WithMessage("Current position cannot exceed 200 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Request.CurrentPosition));

            RuleFor(x => x.Request.LinkedInUrl)
                .MaximumLength(500)
                .WithMessage("LinkedIn URL cannot exceed 500 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Request.LinkedInUrl));

            RuleFor(x => x.Request.Notes)
                .MaximumLength(2000)
                .WithMessage("Notes cannot exceed 2000 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Request.Notes));

            RuleFor(x => x.Request.CurrentSalary)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Current salary cannot be negative.")
                .When(x => x.Request.CurrentSalary.HasValue);

            RuleFor(x => x.Request.ExpectedSalary)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Expected salary cannot be negative.")
                .When(x => x.Request.ExpectedSalary.HasValue);
        }
    }
}
