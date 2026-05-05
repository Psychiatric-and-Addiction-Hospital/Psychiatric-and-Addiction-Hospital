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
    public class CreateCandidateService : ICreateCandidate
    {
        private readonly AddIdentityDbContext _Context;
        public CreateCandidateService(AddIdentityDbContext context)
        {
            _Context = context;
        }
        public async Task<BaseResponse<CandidateResponse>> CreateCandidateAsync(string fullName, string email, string phone, IFormFile resumeUrl, CancellationToken ct)
        {
            var Candidate = new Domain.Entites.HR.Candidate
            {
                FullName = fullName,
                Email = email,
                Phone = phone,
                ResumeUrl = resumeUrl.FileName
            };
            await _Context.AddAsync(Candidate, ct);
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
