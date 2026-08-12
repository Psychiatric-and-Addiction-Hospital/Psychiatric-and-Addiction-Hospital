using Application.Commands.HR.ApplicationInterview;
using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationInterview;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationInterview
{
    public class CreateApplicationInterviewHandler : IRequestHandler<CreateApplicationInterviewCommand, BaseResponse<ApplicationInterviewResponse>>
    {
        private readonly ICreateApplicationInterview _service;
        public CreateApplicationInterviewHandler(ICreateApplicationInterview service)
        {
            _service = service;
        }
        public async Task<BaseResponse<ApplicationInterviewResponse>> Handle(CreateApplicationInterviewCommand request, CancellationToken cancellationToken)
        {
            return await _service.CreateAsync(request, cancellationToken);
        }
    }
}
