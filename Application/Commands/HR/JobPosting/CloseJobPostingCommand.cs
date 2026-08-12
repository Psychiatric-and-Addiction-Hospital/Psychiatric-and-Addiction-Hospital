using Application.Common.Responses;
using MediatR;
using System;

namespace Application.Commands.HR.JobPosting
{
    public record CloseJobPostingCommand(Guid JobPostingId)
     : IRequest<BaseResponse<bool>>;
}
