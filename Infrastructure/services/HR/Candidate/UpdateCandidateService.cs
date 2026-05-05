using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Entites.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Candidate
{
    public class UpdateCandidateService : IUpdateCandidate
    {
        private readonly AddIdentityDbContext _Context;
        public UpdateCandidateService(AddIdentityDbContext context)
        {
            _Context = context;
        }
        public async Task<BaseResponse<CandidateResponse>> UpdateCandidateAsync(
            Guid candidateId, string fullName, string email, string phone, IFormFile resumeUrl, CancellationToken ct)
        {
            var Candidate= _Context.Candidates.FirstOrDefault(c => c.Id == candidateId);
            if (Candidate is null)
                return ResponseFactory.Fail<CandidateResponse>("Candidate Not Found");
            Candidate.FullName = fullName;
            Candidate.Email= email;
            Candidate.Phone = phone;
            Candidate.ResumeUrl= resumeUrl.FileName;
            await _Context.SaveChangesAsync(ct);
            return ResponseFactory.Success(new CandidateResponse
            {
                Id = Candidate.Id,
                FullName = Candidate.FullName,
                Email = Candidate.Email,
                Phone = Candidate.Phone,
                ResumeUrl = Candidate.ResumeUrl
            });


        }
    }
}
