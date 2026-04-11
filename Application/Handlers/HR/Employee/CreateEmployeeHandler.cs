using Application.Commands.HR.Employee;
using Application.Common.Interfaces.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Employee
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, BaseResponse<EmployeeResponse>>
    {
        private readonly ICreateEmployee _createService;

        public CreateEmployeeHandler(ICreateEmployee createService)
        {
            _createService = createService;
        }

        public async Task<BaseResponse<EmployeeResponse>> Handle(CreateEmployeeCommand request, CancellationToken ct)
        {
            return await _createService.CreateAsync(request.UserId, request.EmployeeCode, request.FirstName, request.LastName, request.Email, request.DepartmentId, ct);
        }
    }
}
