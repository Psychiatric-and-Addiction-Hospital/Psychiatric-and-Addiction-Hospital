using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationInterview;
using Application.Queries.HR.ApplicationInterview;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationInterview
{
    public class GetApplicationInterviewByIdHandler : IRequestHandler<GetApplicationInterviewByIdQuery, BaseResponse<ApplicationInterviewResponse>>
    {
        private readonly IGetApplicationInterviewById _service;
        public GetApplicationInterviewByIdHandler(IGetApplicationInterviewById service)
        {
            _service = service;
        }
        public async Task<BaseResponse<ApplicationInterviewResponse>> Handle(GetApplicationInterviewByIdQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetByIdAsync(request, cancellationToken);
        }
    }
}
