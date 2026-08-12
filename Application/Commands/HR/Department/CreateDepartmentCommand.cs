using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;

namespace Application.Commands.HR.Department
{
    public record CreateDepartmentCommand(string Name, string Description)
        : IRequest<BaseResponse<DepartmentResponse>>;
}
