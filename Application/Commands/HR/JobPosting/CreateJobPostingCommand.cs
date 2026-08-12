using Application.Common.Responses;
using Application.DTOS.Request.HR.JobPosting;
using Application.DTOS.Responses.HR.JobPosting;
using MediatR;


namespace Application.Commands.HR.JobPosting
{
    public record CreateJobPostingCommand(CreateJobPostingRequest Request) : IRequest<BaseResponse<JobPostingResponse>>;


}
