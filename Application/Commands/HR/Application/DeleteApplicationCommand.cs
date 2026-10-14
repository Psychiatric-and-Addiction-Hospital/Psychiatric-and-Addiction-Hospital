using Application.Common.Responses;
using MediatR;
using System;

namespace Application.Commands.HR.Application
{
    public record DeleteApplicationCommand(Guid Id) : IRequest<BaseResponse<bool>>;
}
