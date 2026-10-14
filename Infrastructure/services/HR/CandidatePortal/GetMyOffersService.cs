using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.CandidatePortal
{
    public class GetMyOffersService : IGetMyOffers
    {
        private readonly AddIdentityDbContext _context;
        private readonly ICurrentUser _currentUser;

        public GetMyOffersService(AddIdentityDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<List<ApplicationOfferResponse>>> GetAsync(CancellationToken ct)
        {
            if(!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<List<ApplicationOfferResponse>>("User is not authenticated.");

            var userId = _currentUser.UserId;

            if (string.IsNullOrWhiteSpace(userId))
                return ResponseFactory.Fail<List<ApplicationOfferResponse>>("User is not authenticated.");

            var candidate = await _context.Candidates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AppUserId == userId, ct);

            if (candidate == null)
                return ResponseFactory.Fail<List<ApplicationOfferResponse>>("Candidate profile was not found.");

            var offers = await _context.ApplicationOffers
                .AsNoTracking()
                .Where(x =>
                    x.Application.CandidateId == candidate.Id)
                .Include(x => x.Application)
                    .ThenInclude(x => x.JobPosting)
                        .ThenInclude(x => x.Department)
                .Include(x => x.Application)
                    .ThenInclude(x => x.JobPosting)
                        .ThenInclude(x => x.Position)
                .Include(x => x.Contract)
                .OrderByDescending(x => x.OfferDate)
                .Select(x => new ApplicationOfferResponse
                {
                    Id = x.Id,
                    ApplicationId = x.ApplicationId,

                    CandidateId = x.Application.CandidateId,
                    CandidateName = x.Application.Candidate.FullName,

                    JobPostingId = x.Application.JobPostingId,
                    JobTitle = x.Application.JobPosting.Title,

                    DepartmentName = x.Application.JobPosting.Department.Name,

                    PositionName = x.Application.JobPosting.Position.Name,

                    OfferedSalary = x.OfferedSalary,
                    OfferDate = x.OfferDate,
                    ExpiryDate = x.ExpiryDate,
                    ResponseDate = x.ResponseDate,

                    Status = x.Status,

                    Notes = x.Notes,

                    ApprovedByEmployeeId = x.ApprovedByEmployeeId,

                    HasContract = x.Contract != null
                })
                .ToListAsync(ct);

            return ResponseFactory.Success(offers, "Candidate offers retrieved successfully.");
        }
    }
}