using Application.Commands.HR.Department;
using FluentValidation;

namespace Application.Validators.HR.Department
{
    public class DeleteDepartmentCommandValidator : AbstractValidator<DeleteDepartmentCommand>
    {
        public DeleteDepartmentCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Department Id is required.");
        }
    }
}
