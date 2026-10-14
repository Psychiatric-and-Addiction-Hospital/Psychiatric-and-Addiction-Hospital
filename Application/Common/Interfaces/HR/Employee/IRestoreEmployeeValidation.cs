using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using System.Threading;
using System.Threading.Tasks;
using employeeEntity = Domain.Entites.HR.Employee;

namespace Application.Common.Interfaces.HR.Employee
{
    public interface IRestoreEmployeeValidation
    {
        Task<BaseResponse<employeeEntity>> ValidateAsync(RestoreEmployeeRequest request, CancellationToken ct);
    }
}
