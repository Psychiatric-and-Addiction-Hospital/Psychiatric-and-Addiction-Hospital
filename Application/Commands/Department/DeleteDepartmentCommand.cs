using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;


namespace Application.Commands.Department
{
    public record DeleteDepartmentCommand(Guid Id) : IRequest<BaseResponse<DepertmentResponse>>;
   
}
