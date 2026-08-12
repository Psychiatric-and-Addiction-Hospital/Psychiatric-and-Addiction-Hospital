using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Dashboard
{
    public interface IGetEmployeesByDepartment
    {
        Task<BaseResponse<List<EmployeesByDepartmentResponse>>> GetAsync(CancellationToken ct);
    }
}
