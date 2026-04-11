using Application.Commands.HR.Employee;
using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Employee
{
    public class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeCommand, BaseResponse<EmployeeResponse>>
    {
        private readonly IDeleteEmployee _deleteService;

        public DeleteEmployeeHandler(IDeleteEmployee deleteService)
        {
            _deleteService = deleteService;
        }

        public async Task<BaseResponse<EmployeeResponse>> Handle(DeleteEmployeeCommand request, CancellationToken ct)
        {
            return await _deleteService.DeleteEmployee(request.id, ct);
        }
    }
}
