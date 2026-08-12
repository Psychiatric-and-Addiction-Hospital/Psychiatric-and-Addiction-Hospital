using Application.Commands.HR.JobPosting;
using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.JobPosting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.JobPosting
{
    public class UpdateJobPostingHandler : IRequestHandler<UpdateJobPostingCommand, BaseResponse<JobPostingResponse>>
    {
        private readonly IUpdateJobPosting _service;

        public UpdateJobPostingHandler(IUpdateJobPosting service)
        {
            _service = service;
        }

        public async Task<BaseResponse<JobPostingResponse>> Handle(UpdateJobPostingCommand request, CancellationToken ct)
        {
            return await _service.UpdateAsync(request, ct);
        }
    }
}
