using Application.Commands.HR.ApplicationInterview;
using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationInterview
{
    public class CreateApplicationInterviewHandler : IRequestHandler<CreateApplicationInterviewCommand, BaseResponse<ApplicationInterviewResponse>>
    {
        private readonly ICreateApplicationInterview _CreateInterview;
        public CreateApplicationInterviewHandler(ICreateApplicationInterview CreateInterview)
        {
            _CreateInterview = CreateInterview;
        }
        public async Task<BaseResponse<ApplicationInterviewResponse>> Handle(CreateApplicationInterviewCommand request, CancellationToken ct)
        {
            return await _CreateInterview.CreateApplicationInterviewAsync(request.ApplicationProcessId, request.ScheduledTime, request.InterviewerName, request.InterviewType, request.Location,ct);
        }
    }
}
