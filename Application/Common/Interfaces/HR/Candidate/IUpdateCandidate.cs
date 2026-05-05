using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Candidate
{
    public interface IUpdateCandidate
    {
        Task<BaseResponse<CandidateResponse>> UpdateCandidateAsync(
            Guid candidateId, string fullName, string email, string phone, IFormFile resumeUrl, CancellationToken ct);
    }
}
