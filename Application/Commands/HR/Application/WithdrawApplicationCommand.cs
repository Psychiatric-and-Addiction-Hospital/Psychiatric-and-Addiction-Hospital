using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using MediatR;
using System;

namespace Application.Commands.HR.Application
{
    public record WithdrawApplicationCommand(Guid Id) : IRequest<BaseResponse<ApplicationResponse>>;
}
