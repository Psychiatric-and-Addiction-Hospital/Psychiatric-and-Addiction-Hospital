using Application.Common.Responses;
using Application.DTOS.Responses;
using MediatR;
using System;

namespace Application.Commands.Service
{
    public record DeleteServiceCommand(Guid Id): IRequest<BaseResponse<ServiceResponse>>;

}
