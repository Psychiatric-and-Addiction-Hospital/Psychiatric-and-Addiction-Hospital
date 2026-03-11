using Application.Common.Responses;
using Application.DTOS.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Services
{
    public interface IUpdateService
    {
        Task<BaseResponse<ServiceResponse>> UpdateAsync(Guid id, string name, string description, Guid DepartmentId, CancellationToken ct);
    }
}
