using Application.Common.Responses;
using Application.DTOS.Responses.HR.Employee;
using MediatR;
using System;

namespace Application.Queries.HR.Employee
{
    public record GetEmployeeByIdQuery(Guid EmployeeId) : IRequest<BaseResponse<EmployeeResponse>>;
}
