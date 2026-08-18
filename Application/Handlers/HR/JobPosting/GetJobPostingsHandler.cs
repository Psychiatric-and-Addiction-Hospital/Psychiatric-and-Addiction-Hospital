using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.JobPosting;
using Application.Queries.HR.JobPosting;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.JobPosting
{
    public class GetJobPostingsHandler : IRequestHandler<GetJobPostingsQuery, BaseResponse<PagedResponse<JobPostingResponse>>>
    {
        private readonly IGetJobPostings _service;
        public GetJobPostingsHandler(IGetJobPostings service)
        {
            _service = service;
        }
        public async Task<BaseResponse<PagedResponse<JobPostingResponse>>> Handle(GetJobPostingsQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetAllAsync(request.Request, cancellationToken);
        }
    }
}
