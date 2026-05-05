using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.HR.Candidate
{
    public interface ICreateCandidate
    {
        Task<BaseResponse<CandidateResponse>> CreateCandidateAsync(
            string fullName, string email, string phone, IFormFile resumeUrl,CancellationToken ct);
    }
}
