using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Employee
{
    public interface IRestoreEmployee
    {
        Task<BaseResponse<bool>> RestoreAsync(RestoreEmployeeRequest request, CancellationToken ct);
    }
}
