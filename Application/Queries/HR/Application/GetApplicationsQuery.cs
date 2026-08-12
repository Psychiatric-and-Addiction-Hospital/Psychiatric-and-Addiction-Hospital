using Application.Common.Responses;
using Application.DTOS.Request.HR.Application;
using Application.DTOS.Responses.HR.Application;
using MediatR;

namespace Application.Queries.HR.Application
{
    public record GetApplicationsQuery(ApplicationListRequest Request) : IRequest<BaseResponse<PagedResponse<ApplicationResponse>>>;

}
