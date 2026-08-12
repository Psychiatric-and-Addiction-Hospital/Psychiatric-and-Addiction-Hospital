using Application.Commands.HR.Attendance;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.HR.Attendance
{
    public class ManualAttendanceRequestValidator : AbstractValidator<ManualAttendanceCommand>
    {
        public ManualAttendanceRequestValidator()
        {
            RuleFor(x => x.Request.EmployeeId)
              .NotEmpty()
              .WithMessage("Employee ID is required.");

            RuleFor(x => x.Request.AttendanceDate)
                .NotEmpty()
                .WithMessage("Attendance date is required.");

            RuleFor(x => x.Request.CheckOutTime)
                .GreaterThan(x => x.Request.CheckInTime)
                .When(x =>
                    x.Request.CheckInTime.HasValue &&
                    x.Request.CheckOutTime.HasValue)
                .WithMessage("Check-out time must be later than check-in time.");

            RuleFor(x => x.Request.Remarks)
                .MaximumLength(1000)
                .WithMessage("Remarks cannot exceed 1000 characters.");

            RuleFor(x => x.Request.ModificationReason)
                .MaximumLength(500)
                .WithMessage("Modification reason cannot exceed 500 characters.");
        }
    }
}
