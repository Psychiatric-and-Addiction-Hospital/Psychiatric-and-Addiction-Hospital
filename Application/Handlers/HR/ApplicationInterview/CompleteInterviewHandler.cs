using Application.Commands.HR.ApplicationInterview;
using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationInterview;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.ApplicationInterview
{
    public class CompleteInterviewHandler : IRequestHandler<CompleteInterviewCommand, BaseResponse<ApplicationInterviewResponse>>
    {
        private readonly ICompleteApplicationInterview _service;

        public CompleteInterviewHandler(
            ICompleteApplicationInterview service)
        {
            _service = service;
        }

        public async Task<BaseResponse<ApplicationInterviewResponse>> Handle(
            CompleteInterviewCommand request,
            CancellationToken cancellationToken)
        {
            return await _service.CompleteAsync(request, cancellationToken);

        }
    }
}
