using Application.Common.Responses;
using Application.DTOS.Request.EmployeeProfile;
using Application.DTOS.Responses.HR.Employee;
using MediatR;

namespace Application.Commands.EmployeeProfile
{
    public record UpdateMyProfileCommand(UpdateMyProfileRequest request) : IRequest<BaseResponse<EmployeeResponse>>;
}
