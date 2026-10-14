using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveType;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.LeaveType
{
    public interface IDeleteLeaveType
    {
        Task<BaseResponse<bool>> DeleteAsync(DeleteLeaveTypeRequest request, CancellationToken ct);
    }
}
