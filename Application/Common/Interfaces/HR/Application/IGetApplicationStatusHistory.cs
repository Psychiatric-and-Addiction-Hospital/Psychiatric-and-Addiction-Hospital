using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Application
{
    public interface IGetApplicationStatusHistory
    {
        Task<BaseResponse<List<ApplicationStatusHistoryResponse>>> GetAsync(Guid applicationId, CancellationToken ct);
    }
}
