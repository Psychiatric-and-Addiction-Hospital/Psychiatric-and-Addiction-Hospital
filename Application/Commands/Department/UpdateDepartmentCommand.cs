using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;


namespace Application.Commands.Department
{
    public record UpdateDepartmentCommand(Guid Id, string Name, string Description)
        : IRequest<BaseResponse<DepertmentResponse>>;

}
