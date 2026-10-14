using Application.Common.Responses;
using Application.DTOS.Request.HR.JobPosting;
using Application.DTOS.Responses.HR.JobPosting;
using MediatR;

namespace Application.Commands.HR.JobPosting
{
    public record UpdateJobPostingCommand(
       UpdateJobPostingRequest Request) : IRequest<BaseResponse<JobPostingResponse>>;
}
