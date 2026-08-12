using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationInterview;
using Application.Queries.HR.ApplicationInterview;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationInterview
{
    public class GetApplicationInterviewsHandler :
        IRequestHandler<GetApplicationInterviewsQuery, BaseResponse<PagedResponse<ApplicationInterviewResponse>>>
    {
        private readonly IGetApplicationInterviews _service;

        public GetApplicationInterviewsHandler(
            IGetApplicationInterviews service)
        {
            _service = service;
        }

        public async Task<BaseResponse<PagedResponse<ApplicationInterviewResponse>>> Handle(
            GetApplicationInterviewsQuery request,
            CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(
                request.Request,
                cancellationToken);
        }
    }
}
