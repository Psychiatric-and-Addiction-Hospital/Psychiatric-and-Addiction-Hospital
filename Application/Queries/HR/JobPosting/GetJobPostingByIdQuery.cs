using Application.Common.Responses;
using Application.DTOS.Responses.HR.JobPosting;
using MediatR;
using System;

namespace Application.Queries.HR.JobPosting
{

    public record GetJobPostingByIdQuery(Guid Id) : IRequest<BaseResponse<JobPostingResponse>>;
}
