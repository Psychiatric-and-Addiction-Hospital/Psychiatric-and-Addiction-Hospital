using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Employee;
using Application.Queries.HR.Employee;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Employee
{
    public class GetEmployeesHandler : IRequestHandler<GetEmployeesQuery, BaseResponse<PagedResponse<EmployeeResponse>>>
    {
        private readonly IGetEmployees _service;
        public GetEmployeesHandler(IGetEmployees service)
        {
            _service = service;
        }

        public async Task<BaseResponse<PagedResponse<EmployeeResponse>>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(request.request, cancellationToken);
        }
    }
}
