using Application.Commands.Service;
using FluentValidation;

namespace Application.Validators.Service
{
    public class DeleteServiceCommandValidator : AbstractValidator<DeleteServiceCommand>
    {
        public DeleteServiceCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Service Id is required.");
        }
    }
}
