using Application.Common.Responses;
using Application.DTOS.Responses.HR.LeaveType;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.LeaveType
{
    public interface IGetleaveTypeById
    {
        Task<BaseResponse<LeaveTypeResponse>> GetByIdAsync(Guid id, CancellationToken ct);
    }
}
