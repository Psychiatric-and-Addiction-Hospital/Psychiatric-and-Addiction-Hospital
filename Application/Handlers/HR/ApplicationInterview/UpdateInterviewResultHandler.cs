using Application.Commands.HR.ApplicationInterview;
using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationInterview
{
    public class UpdateInterviewResultHandler : IRequestHandler<UpdateInterviewResultCommand, BaseResponse<ApplicationInterviewResponse>>
    {
        private readonly IUpdateInterviewResult _interviewResult;
        public UpdateInterviewResultHandler(IUpdateInterviewResult interviewResult)
        {
            _interviewResult = interviewResult;
        }
        public async Task<BaseResponse<ApplicationInterviewResponse>> Handle(UpdateInterviewResultCommand request, CancellationToken ct)
        {
            return await _interviewResult.UpdateInterviewResult(request.InterviewId, request.Feedback, request.Score,ct);
        }
    }
}
