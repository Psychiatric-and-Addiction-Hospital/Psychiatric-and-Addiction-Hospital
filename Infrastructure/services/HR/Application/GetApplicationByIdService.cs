using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.Application
{
    public class GetApplicationByIdService : IGetApplicationById
    {
        private readonly AddIdentityDbContext _context;
        public GetApplicationByIdService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<ApplicationResponse>> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var application = await _context.Applications
                .AsNoTracking()
                .Include(x => x.Candidate)
                .Include(x => x.JobPosting)
                .ThenInclude(x => x.Department)
                .Include(x => x.JobPosting)
                .ThenInclude(x => x.Position)
                .Include(x => x.Interviews)
                .Include(x => x.Offer)
                .FirstOrDefaultAsync(x => x.Id == id, ct);

            if (application == null)
            {
                return ResponseFactory.Fail<ApplicationResponse>("Application not found.");
            }

        return ResponseFactory.Success(new ApplicationResponse
            {
                Id = application.Id,
                CandidateId = application.CandidateId,
                CandidateName = application.Candidate.FullName,
                JobPostingId= application.JobPostingId,
                JobTitle = application.JobPosting.Title,
                DepartmentName = application.JobPosting.Department.Name,
                PositionName = application.JobPosting.Position.Name,
                AppliedDate = application.AppliedDate,
                Status = application.Status,
                Notes = application.Notes,
                CoverLetter = application.CoverLetter,
                ResumeSnapshotUrl = application.ResumeSnapshotUrl,
                InterviewsCount = application.Interviews.Count,
                HasOffer = application.Offer != null
            }, "Application retrieved successfully.");
        }
    }
}
