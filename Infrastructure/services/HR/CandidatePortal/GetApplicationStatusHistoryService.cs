using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Application;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.CandidatePortal
{
    public class GetApplicationStatusHistoryService : IGetApplicationStatusHistory
    {
        private readonly AddIdentityDbContext _context;
        private readonly ICurrentUser _currentUser;

        public GetApplicationStatusHistoryService(AddIdentityDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<BaseResponse<List<ApplicationStatusHistoryResponse>>> GetAsync(Guid applicationId,CancellationToken ct)
        {
            if (!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<List<ApplicationStatusHistoryResponse>>("User must be authenticated.");

            var userId = _currentUser.UserId;

            if (string.IsNullOrWhiteSpace(userId))

                return ResponseFactory.Fail<List<ApplicationStatusHistoryResponse>>("Authenticated user must have a valid user ID.");

            var candidate = await _context.Candidates
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    x => x.AppUserId == userId, ct);

            if (candidate == null)
                return ResponseFactory.Fail<List<ApplicationStatusHistoryResponse>>("Candidate profile was not found.");


            var applicationExists = await _context.Applications
                .AsNoTracking()
                .AnyAsync(
                    x => x.Id == applicationId &&
                         x.CandidateId == candidate.Id,
                    ct);

            if (!applicationExists)
            {
                return ResponseFactory.Fail<List<ApplicationStatusHistoryResponse>>(
                    "Application was not found.");
            }

            var history = await _context.ApplicationStatusHistorys
                .AsNoTracking()
                .Where(x => x.ApplicationId == applicationId)
                .OrderBy(x => x.ChangedAt)
                .Select(x => new ApplicationStatusHistoryResponse
                {
                    Id = x.Id,
                    ApplicationId = x.ApplicationId,
                    Status = x.Status,
                    ChangedAt = x.ChangedAt,
                    Notes = x.Notes
                })
                .ToListAsync(ct);
            return ResponseFactory.Success(history,"Application status history retrieved successfully.");

        }
    }
}