using Application.DTOS.Request.HR.LeaveType;
using FluentValidation;

namespace Application.Validators.HR.LeaveType
{
    public class CreateLeaveTypeValidator : AbstractValidator<CreateLeaveTypeRequest>
    {
        public CreateLeaveTypeValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.MaxDaysPerYear)
                .GreaterThanOrEqualTo(0);
        }
    }
}
