using Application.Common.Responses;
using Application.DTOS.Responses.HR.Employee;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.EmployeeProfile
{
    public interface IGetMyProfile
    {
        Task<BaseResponse<EmployeeResponse>> GetAsync(CancellationToken ct);
    }
}
