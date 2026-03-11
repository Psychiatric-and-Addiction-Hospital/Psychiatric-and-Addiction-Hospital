using Application.Common.Responses;
using FluentResults;
using MediatR;
using System;

namespace Application.Commands.Admin
{
    public record ApproveDoctorCommand
   (Guid ApplicationId, Guid DepartmentId) : IRequest<BaseResponse<string>>;
}
