using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.Employee
{
    public record GetAllEmployeesQuery() : IRequest<BaseResponse<List<EmployeeResponse>>>;
}
