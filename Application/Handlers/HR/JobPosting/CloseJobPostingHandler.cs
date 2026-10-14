using Application.Commands.HR.JobPosting;
using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.JobPosting
{
    public class CloseJobPostingHandler : IRequestHandler<CloseJobPostingCommand, BaseResponse<bool>>
    {
        private readonly ICloseJobPosting _service;

        public CloseJobPostingHandler(ICloseJobPosting service)
        {
            _service = service;
        }

        public async Task<BaseResponse<bool>> Handle(CloseJobPostingCommand request, CancellationToken ct)
        {
            return await _service.CloseAsync(request.JobPostingId,ct);
        }
    }
}
