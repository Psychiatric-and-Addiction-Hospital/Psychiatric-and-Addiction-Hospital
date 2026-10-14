using Application.Common.Responses;
using MediatR;
using System;

namespace Application.Commands.HR.ApplicationInterview
{
    public record DeleteApplicationInterviewCommand(Guid Id)
         : IRequest<BaseResponse<bool>>;
}
