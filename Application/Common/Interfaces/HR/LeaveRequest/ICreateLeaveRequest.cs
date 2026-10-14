using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveRequest;
using Application.DTOS.Responses.HR.LeaveRequest;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.LeaveRequest
{
    public interface ICreateLeaveRequest
    {
        Task<BaseResponse<LeaveRequestResponse>> CreateAsync(CreateLeaveRequest request, CancellationToken ct);
    }
}
