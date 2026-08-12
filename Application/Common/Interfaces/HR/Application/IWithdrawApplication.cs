using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Application
{
    public interface IWithdrawApplication
    {
        Task<BaseResponse<ApplicationResponse>> WithdrawAsync(Guid applicationId, CancellationToken ct);
    }
}
