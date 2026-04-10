using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.Employee;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Employee
{
    public class GetByIDEmployeeHandler : IRequestHandler<GetEmployeeByIdQuery, BaseResponse<EmployeeResponse>>
    {
        private readonly IGetEmployee _getEmployee;

        public GetByIDEmployeeHandler(IGetEmployee getEmployee)
        {
            _getEmployee = getEmployee;
        }

        public async Task<BaseResponse<EmployeeResponse>> Handle(GetEmployeeByIdQuery request, CancellationToken ct)
        {
            return await _getEmployee.GetEmployee(request.Id, ct);
        }
    }
}
