
using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Application.Queries.HR.Candidate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers.HR.Candidate
{
    public class GetCandidateByIdHandler : IRequestHandler<GetCandidateByIdQuery, BaseResponse<CandidateResponse>>
    {
        private readonly IGetCandidateById _getCandidateById;
        public GetCandidateByIdHandler(IGetCandidateById getCandidateById)
        {
            _getCandidateById = getCandidateById;
        }
        public async Task<BaseResponse<CandidateResponse>> Handle(GetCandidateByIdQuery request, CancellationToken ct)
        {
            return await _getCandidateById.GetCandidateByIdAsync(request.Id, ct);
        }
    }
}
