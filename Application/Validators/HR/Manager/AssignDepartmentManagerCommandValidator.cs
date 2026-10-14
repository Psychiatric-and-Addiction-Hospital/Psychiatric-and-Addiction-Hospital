using Application.Commands.HR.Manager;
using FluentValidation;

namespace Application.Validators.HR.Manager
{
    public class AssignDepartmentManagerCommandValidator : AbstractValidator<AssignDepartmentManagerCommand>
    {
        public AssignDepartmentManagerCommandValidator()
        {
            RuleFor(x => x.request.DepartmentId)
                .NotEmpty().WithMessage("DepartmentId is required.");

            RuleFor(x => x.request.EmployeeId)
                .NotEmpty().WithMessage("EmployeeId is required.");
        }
    }
}
