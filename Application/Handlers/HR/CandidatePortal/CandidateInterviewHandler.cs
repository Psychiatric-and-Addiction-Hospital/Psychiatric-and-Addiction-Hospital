using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Candidate;
using Application.Queries.HR.CandidatePortal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.CandidatePortal
{
    public class CandidateInterviewHandler : IRequestHandler<CandidateInterviewQuery, BaseResponse<List<CandidateInterviewResponse>>>
    {
        private readonly ICandidateInterview _service;
        public CandidateInterviewHandler(ICandidateInterview service)
        {
            _service = service;
        }
        public async Task<BaseResponse<List<CandidateInterviewResponse>>> Handle(CandidateInterviewQuery request, CancellationToken cancellationToken)
        {
            return await _service.GetUpcomingAsync(cancellationToken);
        }
    }
}
