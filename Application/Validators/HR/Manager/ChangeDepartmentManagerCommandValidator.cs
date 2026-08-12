using Application.Commands.HR.Manager;
using FluentValidation;

namespace Application.Validators.HR.Manager
{
    public class ChangeDepartmentManagerCommandValidator : AbstractValidator<ChangeDepartmentManagerCommand>
    {
        public ChangeDepartmentManagerCommandValidator()
        {
            RuleFor(x => x.request.DepartmentId)
                .NotEmpty().WithMessage("DepartmentId is required.");

            RuleFor(x => x.request.NewManagerId)
                .NotEmpty().WithMessage("NewManagerId is required.");
        }
    }
}
