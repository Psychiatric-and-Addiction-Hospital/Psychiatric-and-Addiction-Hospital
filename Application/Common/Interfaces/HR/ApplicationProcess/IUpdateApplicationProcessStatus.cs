using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Enums;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.ApplicationProcess
{
    public interface IUpdateApplicationProcessStatus
    {
        Task<BaseResponse<ApplicationProcessResponse>> UpdateApplicationProcessStatus(Guid applicationProcessId, ApplicationStatus status,CancellationToken ct);
    }
}