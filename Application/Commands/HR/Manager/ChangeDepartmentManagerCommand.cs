using Application.Common.Responses;
using Application.DTOS.Request.HR.manager;
using Application.DTOS.Responses.HR.Manager;
using MediatR;

namespace Application.Commands.HR.Manager
{
    public record ChangeDepartmentManagerCommand(ChangeDepartmentManagerRequest request) : IRequest<BaseResponse<DepartmentManagerResponse>>;
}
