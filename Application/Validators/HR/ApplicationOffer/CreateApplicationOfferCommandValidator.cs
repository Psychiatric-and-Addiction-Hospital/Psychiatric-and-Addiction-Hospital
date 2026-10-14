using Application.Commands.HR.ApplicationOffer;
using FluentValidation;
using System;

namespace Application.Validators.HR.ApplicationOffer
{
    public class CreateApplicationOfferCommandValidator:AbstractValidator<CreateApplicationOfferCommand>
    {
        public CreateApplicationOfferCommandValidator() 
        {
            RuleFor(x => x.request.ApplicationId)
                .NotEmpty()
                .WithMessage("ApplicationId is required.");

            RuleFor(x => x.request.OfferedSalary)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Offered salary must be greater than or equal to 0.");

            RuleFor(x => x.request.OfferDate)
                .NotEmpty()
                .WithMessage("Offer date is required.");

            RuleFor(x => x.request.ExpiryDate)
                .NotEmpty()
                .WithMessage("Expiry date is required.")
                .GreaterThan(x => x.request.OfferDate)
                .WithMessage("Expiry date must be later than the offer date.");

            RuleFor(x => x.request.Notes)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrWhiteSpace(x.request.Notes))
                .WithMessage("Notes cannot exceed 1000 characters.");

            RuleFor(x => x.request.ApprovedByEmployeeId)
                .Must(id => id == null || id != Guid.Empty)
                .WithMessage("ApprovedByEmployeeId cannot be an empty Guid.");
        }
    }
}
