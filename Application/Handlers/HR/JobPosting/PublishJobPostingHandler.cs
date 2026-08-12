using Application.Commands.HR.JobPosting;
using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.JobPosting
{
    public class PublishJobPostingHandler : IRequestHandler<PublishJobPostingCommand, BaseResponse<bool>>
    {
        private readonly IPublishJobPosting _publishJobPosting;
        public PublishJobPostingHandler(IPublishJobPosting publishJobPosting)
        {
            _publishJobPosting = publishJobPosting;
        }
        public async Task<BaseResponse<bool>> Handle(PublishJobPostingCommand request, CancellationToken cancellationToken)
        {
           return await _publishJobPosting.PublishAsync(request.JobPostingId, cancellationToken);
        }
    }
}
