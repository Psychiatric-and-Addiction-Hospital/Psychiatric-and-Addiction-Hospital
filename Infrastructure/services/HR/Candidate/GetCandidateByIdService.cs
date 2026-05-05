using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Candidate
{
    public class GetCandidateByIdService : IGetCandidateById
    {
        private readonly AddIdentityDbContext _Context;
        public GetCandidateByIdService(AddIdentityDbContext context)
        {
            _Context = context;
        }
        public async Task<BaseResponse<CandidateResponse>> GetCandidateByIdAsync(Guid id, CancellationToken ct)
        {
            var Candidate = await _Context.Candidates.FirstOrDefaultAsync(c => c.Id == id);
            if (Candidate is null)
                return ResponseFactory.Fail<CandidateResponse>("Candidate Not Found");
            return ResponseFactory.Success(new CandidateResponse
            {
                Id = id,
                FullName = Candidate.FullName,
                Email = Candidate.Email,
                Phone = Candidate.Phone,
                ResumeUrl= Candidate.ResumeUrl,
            }, "Candidate retrieved successfully.");
        }
    }
}
