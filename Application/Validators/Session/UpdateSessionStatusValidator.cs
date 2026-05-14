using Application.Commands.Session;
using Domain.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators.Session
{
    public class UpdateSessionStatusValidator : AbstractValidator<UpdateSessionStatusCommand>
    {
        public UpdateSessionStatusValidator()
        {
            RuleFor(x => x.SessionId).NotEmpty();
            // التحقق من وجود سبب في حالة الإلغاء
            RuleFor(x => x.Reason)
                .NotEmpty()
                .When(x => x.NewStatus == SessionStatus.Cancelled)
                .WithMessage("You should enter the cancelled reason!");
        }
    }
}
