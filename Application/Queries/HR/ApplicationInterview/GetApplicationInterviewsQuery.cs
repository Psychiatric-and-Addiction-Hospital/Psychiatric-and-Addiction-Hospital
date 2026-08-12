using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationInterview;
using Application.DTOS.Responses.HR.ApplicationInterview;
using MediatR;

namespace Application.Queries.HR.ApplicationInterview
{
    public record GetApplicationInterviewsQuery(ApplicationInterviewListRequest Request)
        : IRequest<BaseResponse<PagedResponse<ApplicationInterviewResponse>>>;
}
