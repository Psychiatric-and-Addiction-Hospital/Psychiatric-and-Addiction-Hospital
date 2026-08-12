using Application.Commands.HR.Employee;
using FluentValidation;

namespace Application.Validators.HR.Employee
{
    public class DeleteEmployeeCommandValidator:AbstractValidator<DeleteEmployeeCommand>
    {
        public DeleteEmployeeCommandValidator()
        {
            RuleFor(x => x.request.EmployeeId)
                .NotEmpty()
                .WithMessage("Employee ID is required.");

            RuleFor(x => x.request.Reason)
                .MaximumLength(500)
                .WithMessage("Reason cannot exceed 500 characters.");
        }
    }
}
