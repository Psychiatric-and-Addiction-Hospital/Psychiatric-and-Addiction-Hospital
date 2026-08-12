using Application.Commands.HR.Employee;
using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Employee;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Employee
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, BaseResponse<EmployeeResponse>>
    {
        private readonly IUpdateEmployee _service;
        public UpdateEmployeeHandler(IUpdateEmployee service)
        {
            _service = service;
        }

        public async Task<BaseResponse<EmployeeResponse>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(request.request, cancellationToken);
        }
    }
}
