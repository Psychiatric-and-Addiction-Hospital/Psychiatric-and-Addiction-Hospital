using Application.Common.Responses;
using MediatR;
using System;

namespace Application.Commands.HR.JobPosting
{
    public record PublishJobPostingCommand(Guid JobPostingId)
     : IRequest<BaseResponse<bool>>;
}
