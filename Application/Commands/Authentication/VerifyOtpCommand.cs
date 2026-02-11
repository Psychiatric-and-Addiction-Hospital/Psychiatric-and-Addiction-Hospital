using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Authentication
{
    public record VerifyOtpCommand(string Email, string Code)
        : IRequest<Result<string>>;


}
