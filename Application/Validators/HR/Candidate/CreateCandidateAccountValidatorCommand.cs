using Application.DTOS.Request.HR.Candidate;
using FluentValidation;

namespace Application.Validators.HR.Candidate
{
    public class CreateCandidateAccountValidatorCommand : AbstractValidator<CreateCandidateAccountRequest>
    {
        public CreateCandidateAccountValidatorCommand()
        {
            RuleFor(x => x.CandidateId)
                .NotEmpty()
                .WithMessage("Candidate ID is required.");

            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("Account setup token is required.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .WithMessage("Confirm Password is required.")
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match.");
        }
    }
}
