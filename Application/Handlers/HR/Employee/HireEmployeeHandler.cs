using Application.Commands.HR.Employee;
using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Employee;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Employee
{
    public class HireEmployeeHandler : IRequestHandler<HireEmployeeCommand, BaseResponse<EmployeeResponse>>
    {
        private readonly IHireEmployee _service;
        public HireEmployeeHandler(IHireEmployee service)
        {
            _service = service;
        }

        public async Task<BaseResponse<EmployeeResponse>> Handle(HireEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await _service.HireAsync(request.request, cancellationToken);
        }
    }
}
