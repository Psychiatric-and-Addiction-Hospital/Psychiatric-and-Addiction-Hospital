using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Enums;
using MediatR;
using System;

namespace Application.Commands.HR.ApplicationProcess
{
    public record UpdateApplicationProcessStatusCommand(Guid ApplicationProcessId, ApplicationStatus
        Status) : IRequest<BaseResponse<ApplicationProcessResponse>>;
 
}
