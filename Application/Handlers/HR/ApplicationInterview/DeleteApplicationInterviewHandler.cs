using Application.Commands.HR.ApplicationInterview;
using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationInterview
{
    public class DeleteApplicationInterviewHandler : IRequestHandler<DeleteApplicationInterviewCommand, BaseResponse<bool>>
    {
        private readonly IDeleteApplicationInterview _service;

        public DeleteApplicationInterviewHandler(IDeleteApplicationInterview service)
        {
            _service = service;
        }

        public async Task<BaseResponse<bool>> Handle(DeleteApplicationInterviewCommand request, CancellationToken cancellationToken)
        {
            return await _service.DeleteAsync(request.Id, cancellationToken);
        }
    }
}
