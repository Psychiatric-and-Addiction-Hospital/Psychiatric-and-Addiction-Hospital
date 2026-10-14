using Application.Common.Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.LeaveType
{
    public interface IRestoreLeaveType
    {
        Task<BaseResponse<bool>> RestoreAsync(Guid Id, CancellationToken ct);
    }
}
