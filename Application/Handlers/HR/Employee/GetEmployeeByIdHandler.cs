using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Employee;
using Application.Queries.HR.Employee;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Employee
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, BaseResponse<EmployeeResponse>>
    {
        private readonly IGetEmployeeById _service;

        public GetEmployeeByIdHandler(IGetEmployeeById service)
        {
            _service = service;
        }

        public async Task<BaseResponse<EmployeeResponse>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdAsync(request.EmployeeId, cancellationToken);
        }
    }
}
