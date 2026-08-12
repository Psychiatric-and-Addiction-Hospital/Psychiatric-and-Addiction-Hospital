using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveRequest;
using System.Threading;
using System.Threading.Tasks;
using employee= Domain.Entites.HR.Employee;

namespace Application.Common.Interfaces.HR.LeaveRequest
{
    public interface ILeaveRequestValidation
    {
        Task<BaseResponse<employee>> ValidateCreateAsync(CreateLeaveRequest request, CancellationToken ct);
    }
}
