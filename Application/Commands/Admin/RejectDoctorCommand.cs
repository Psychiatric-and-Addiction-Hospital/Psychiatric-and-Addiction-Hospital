using Application.Common.Responses;
using FluentResults;
using MediatR;
using System;

namespace Application.Commands.Admin
{
    public record RejectDoctorCommand
         (Guid ApplicationId,string Reason) : IRequest<BaseResponse<string>>;

}
