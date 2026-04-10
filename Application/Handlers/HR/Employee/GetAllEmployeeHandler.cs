using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.Employee;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Employee
{
    public class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeesQuery, BaseResponse<List<EmployeeResponse>>>
    {
        private readonly IGetAllEmployee _getAllEmployee;

        public GetAllEmployeeHandler(IGetAllEmployee getAllEmployee)
        {
            _getAllEmployee = getAllEmployee;
        }

        public async Task<BaseResponse<List<EmployeeResponse>>> Handle(GetAllEmployeesQuery request, CancellationToken ct)
        {
            return await _getAllEmployee.GetAllEmployee(ct);
        }
    }
}
