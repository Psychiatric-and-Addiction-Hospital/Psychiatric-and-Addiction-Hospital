using Application.Commands.HR.Employee;
using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Employee
{
    public class RestoreEmployeeHandler : IRequestHandler<RestoreEmployeeCommand, BaseResponse<bool>>
    {

        private readonly IRestoreEmployee _service;
        public RestoreEmployeeHandler(IRestoreEmployee service)
        {
            _service = service;
        }

        public Task<BaseResponse<bool>> Handle(RestoreEmployeeCommand request, CancellationToken cancellationToken)
        {
            return _service.RestoreAsync(request.request, cancellationToken);
        }
    }
}
