using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.JobPosting;
using Application.Queries.HR.JobPosting;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.JobPosting
{
    public class GetJobPostingByIdHandler : IRequestHandler<GetJobPostingByIdQuery, BaseResponse<JobPostingResponse>>
    {
        private readonly IGetJobPostingById _service;
        public GetJobPostingByIdHandler(IGetJobPostingById service)
        {
            _service = service;
        }

        public async Task<BaseResponse<JobPostingResponse>> Handle(GetJobPostingByIdQuery request, CancellationToken ct)
        {
            return await _service.GetByIdAsync(request.Id, ct);
        }
    }
}
