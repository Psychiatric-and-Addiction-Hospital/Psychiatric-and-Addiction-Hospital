using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveType;
using Application.DTOS.Responses.HR.LeaveType;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.LeaveType
{
    public interface ICreateLeaveType
    {
        Task<BaseResponse<LeaveTypeResponse>> CreateAsync(CreateLeaveTypeRequest request, CancellationToken ct);
    }
}
