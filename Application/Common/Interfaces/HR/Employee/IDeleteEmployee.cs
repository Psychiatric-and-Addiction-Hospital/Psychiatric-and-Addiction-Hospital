using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Employee
{
    public interface IDeleteEmployee
    {
        Task<BaseResponse<bool>> DeleteAsync(DeleteEmployeeRequest request, CancellationToken ct);
    }
}
