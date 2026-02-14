using FluentResults;
using MediatR;
using System;

namespace Application.Commands.Admin
{
    public record ApproveDoctorCommand
   (Guid ApplicationId):IRequest<Result<string>>;
}
