using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using Application.DTOS.Responses.HR.Employee;
using MediatR;

namespace Application.Commands.HR.Employee
{
    public record HireEmployeeCommand(HireEmployeeRequest request) : IRequest<BaseResponse<EmployeeResponse>>;
  
}
