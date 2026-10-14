using Application.Common.Responses;
using Application.DTOS.Responses.HR.Employee;
using MediatR;

namespace Application.Queries.EmployeeProfile
{
    public record GetMyProfileQuery() : IRequest<BaseResponse<EmployeeResponse>>;
}
