using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;


namespace Application.Commands.HR.Department
{
    public record UpdateDepartmentCommand(Guid Id, string Name, string Description)
        : IRequest<BaseResponse<DepertmentResponse>>;

}
