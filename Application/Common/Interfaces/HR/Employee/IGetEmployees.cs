using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using Application.DTOS.Responses.HR.Employee;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Employee
{
    public interface IGetEmployees
    {
        Task<BaseResponse<PagedResponse<EmployeeResponse>>> GetAllAsync(EmployeeListRequest request,CancellationToken ct);
    }
}
