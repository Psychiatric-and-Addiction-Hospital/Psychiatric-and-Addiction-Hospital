using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using System.Threading;
using System.Threading.Tasks;
using employeeEntity = Domain.Entites.HR.Employee;

namespace Application.Common.Interfaces.HR.Employee
{
    public interface IEmployeeValidation
    {
        Task<BaseResponse<employeeEntity>> ValidateAsync(UpdateEmployeeRequest request, CancellationToken ct);
    }
}
