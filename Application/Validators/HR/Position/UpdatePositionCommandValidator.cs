using Application.Commands.HR.Position;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.HR.Position
{
    public class UpdatePositionCommandValidator : AbstractValidator<UpdatePositionCommand>
    {
        public UpdatePositionCommandValidator()
        {
            RuleFor(x => x.request.Id)
             .NotEmpty().WithMessage("Id name is required.");

            RuleFor(x => x.request.Name)
                   .NotEmpty().WithMessage("Position name is required.")
                   .MaximumLength(100).WithMessage("Position name cannot exceed 100 characters.");

            RuleFor(x => x.request.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.request.BasicSalary)
                   .GreaterThanOrEqualTo(0)
                   .WithMessage("Basic salary must be greater than zero.");

            RuleFor(x => x.request.DepartmentId)
                .NotEmpty().WithMessage("Department ID is required.");

        }
    }
}
