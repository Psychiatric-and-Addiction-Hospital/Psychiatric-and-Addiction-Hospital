using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;

namespace Application.Queries.Employee
{
    public record GetEmployeeByIdQuery(Guid Id) : IRequest<BaseResponse<EmployeeResponse>>;
}
