using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using System.Threading;
using System.Threading.Tasks;
using ContractEntity = Domain.Entites.HR.Contract;

namespace Application.Common.Interfaces.HR.Employee
{
    public interface IHireEmployeeVaildation
    {
        Task<BaseResponse<ContractEntity>> ValidateHireAsync(HireEmployeeRequest request, CancellationToken ct);
    }
}
