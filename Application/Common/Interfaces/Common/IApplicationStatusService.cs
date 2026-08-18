using Application.Common.Responses;
using Domain.Enums.HR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Common
{
    public interface IApplicationStatusService
    {
        Task<BaseResponse<Domain.Entites.HR.Recruitment.Application>> ChangeStatusAsync
            (Guid applicationId, ApplicationStatus newStatus, string? notes, CancellationToken ct);
    }
}
