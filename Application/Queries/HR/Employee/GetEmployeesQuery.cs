using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using Application.DTOS.Responses.HR.Employee;
using MediatR;


namespace Application.Queries.HR.Employee
{
    public record GetEmployeesQuery(EmployeeListRequest request) : IRequest<BaseResponse<PagedResponse<EmployeeResponse>>>;
}
