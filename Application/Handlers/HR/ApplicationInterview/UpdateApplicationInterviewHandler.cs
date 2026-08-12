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
    internal class UpdateApplicationInterviewHandler
    : IRequestHandler<
        UpdateApplicationInterviewCommand,
        BaseResponse<ApplicationInterviewResponse>>
    {
        private readonly IUpdateApplicationInterview _service;

        public UpdateApplicationInterviewHandler(
            IUpdateApplicationInterview service)
        {
            _service = service;
        }

        public async Task<BaseResponse<ApplicationInterviewResponse>> Handle(UpdateApplicationInterviewCommand request,CancellationToken cancellationToken)
        {
            return await _service.UpdateAsync(request,cancellationToken);
        }
    }
}
