using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;
using MediatR;
using System.Collections.Generic;

namespace Application.Queries.HR.Dashboard
{
    public record GetEmployeesByDepartmentQuery() : IRequest<BaseResponse<List<EmployeesByDepartmentResponse>>>;
}
