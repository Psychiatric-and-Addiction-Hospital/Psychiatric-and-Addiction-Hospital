using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveType;
using System.Threading;
using System.Threading.Tasks;
using leaveType = Domain.Entites.HR.Leave.LeaveType;

namespace Application.Common.Interfaces.HR.LeaveType
{
    public interface ILeaveTypeValidation
    {
        Task<BaseResponse<bool>> ValidateCreateAsync(CreateLeaveTypeRequest request, CancellationToken ct);
        Task<BaseResponse<leaveType>> ValidateUpdateAsync(UpdateLeaveTypeRequest request, CancellationToken ct);
        Task<BaseResponse<leaveType>> ValidateDeleteAsync(DeleteLeaveTypeRequest request, CancellationToken ct);
    }
}
