using Application.Common.Responses;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Authentication
{
    public record ResetPasswordCommand(
        string Email,
        string NewPassword
        )
         : IRequest<BaseResponse<string>>;
    
}
