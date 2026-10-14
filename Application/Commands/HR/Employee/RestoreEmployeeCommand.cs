using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using MediatR;
using System.Threading;

namespace Application.Commands.HR.Employee
{
    public record RestoreEmployeeCommand(RestoreEmployeeRequest request) : IRequest<BaseResponse<bool>>;

}
