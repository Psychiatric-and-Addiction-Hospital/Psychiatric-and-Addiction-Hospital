using Application.Commands.HR.JobPosting;
using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.JobPosting;
using Domain.Entites.ServicesModule;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.JobPosting
{
    public class CreateJobPostingHandler : IRequestHandler<CreateJobPostingCommand, BaseResponse<JobPostingResponse>>
    {
        private readonly ICreateJobPosting _service;

        public CreateJobPostingHandler(ICreateJobPosting service)
        {
            _service = service;
        }
        public async Task<BaseResponse<JobPostingResponse>> Handle(CreateJobPostingCommand request, CancellationToken ct)
        {
            return await _service.CreateAsync(request, ct);
        }
    }
}
