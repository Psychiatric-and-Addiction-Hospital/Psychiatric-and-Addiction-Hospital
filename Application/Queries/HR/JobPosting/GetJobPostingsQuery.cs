using Application.Common.Responses;
using Application.DTOS.Request.HR.JobPosting;
using Application.DTOS.Responses.HR.JobPosting;
using MediatR;

namespace Application.Queries.HR.JobPosting
{
    public record GetJobPostingsQuery(JobPostingListRequest Request) : IRequest<BaseResponse<PagedResponse<JobPostingResponse>>>;

}
