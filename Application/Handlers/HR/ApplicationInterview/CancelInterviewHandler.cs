using Application.Commands.HR.ApplicationInterview;
using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationInterview;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationInterview
{
    public class CancelInterviewHandler : IRequestHandler<CancelInterviewCommand, BaseResponse<ApplicationInterviewResponse>>
    {
        private readonly ICancelApplicationInterview _service;

        public CancelInterviewHandler(ICancelApplicationInterview service)
        {
            _service = service;
        }

        public async Task<BaseResponse<ApplicationInterviewResponse>> Handle(CancelInterviewCommand request, CancellationToken ct)
        {
            return await _service.CancelAsync(request.InterviewId, ct);
        }
    }
}

