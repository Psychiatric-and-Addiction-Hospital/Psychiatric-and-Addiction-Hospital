using Application.Commands.HR.Employee;
using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Employee
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, BaseResponse<EmployeeResponse>>
    {
        private readonly IUpdateEmployee _updateService;

        public UpdateEmployeeHandler(IUpdateEmployee updateService)
        {
            _updateService = updateService;
        }

        public async Task<BaseResponse<EmployeeResponse>> Handle(UpdateEmployeeCommand request, CancellationToken ct)
        {
            return await _updateService.UpdateEmployee(request.UserId, request.EmployeeCode, request.FirstName, request.LastName, request.Email, request.DepartmentId, ct);
        }
    }
}
