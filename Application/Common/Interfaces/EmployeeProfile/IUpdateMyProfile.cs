using Application.Common.Responses;
using Application.DTOS.Request.EmployeeProfile;
using Application.DTOS.Responses.HR.Employee;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.EmployeeProfile
{
    public interface IUpdateMyProfile
    {
        Task<BaseResponse<EmployeeResponse>> UpdateAsync(UpdateMyProfileRequest request, CancellationToken ct);
    }
}
