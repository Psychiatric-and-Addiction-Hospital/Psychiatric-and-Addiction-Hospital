using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Commands.HR.Employee
{
    public record UpdateEmployeeCommand(
        string UserId,
        string EmployeeCode,
        string FirstName,
        string LastName,
        string Email,
        Guid DepartmentId
    ) : IRequest<BaseResponse<EmployeeResponse>>;
}
