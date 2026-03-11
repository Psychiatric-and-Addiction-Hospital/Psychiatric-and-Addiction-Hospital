using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;

namespace Application.Commands.Service
{
    public record UpdateServiceCommand(Guid Id, string Name, string Description, Guid DepartmentId)
        : IRequest<BaseResponse<ServiceResponse>>;
   
}
