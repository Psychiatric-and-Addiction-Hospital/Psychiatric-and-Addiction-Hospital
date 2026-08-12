using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.ApplicationOffer
{
    public class GetApplicationOfferByIdService : IGetApplicationOfferById
    {
        private readonly AddIdentityDbContext _context;

        public GetApplicationOfferByIdService(
            AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<ApplicationOfferResponse>> GetByIdAsync(
            Guid id,
            CancellationToken ct)
        {
            var offer = await _context.ApplicationOffers
                .AsNoTracking()
                .Include(x => x.Application)
                    .ThenInclude(x => x.Candidate)
                .Include(x => x.Application)
                    .ThenInclude(x => x.JobPosting)
                        .ThenInclude(x => x.Department)
                .Include(x => x.Application)
                    .ThenInclude(x => x.JobPosting)
                        .ThenInclude(x => x.Position)
                .Include(x => x.ApprovedByEmployee)
                .Include(x => x.Contract)
                .FirstOrDefaultAsync(x => x.Id == id, ct);

            if (offer == null)
                return ResponseFactory.Fail<ApplicationOfferResponse>("Application offer not found.");


            var response = new ApplicationOfferResponse
            {
                Id = offer.Id,

                ApplicationId = offer.ApplicationId,

                CandidateId = offer.Application.CandidateId,

                CandidateName = offer.Application.Candidate.FullName,

                JobPostingId = offer.Application.JobPostingId,

                JobTitle = offer.Application.JobPosting.Title,

                DepartmentName = offer.Application.JobPosting.Department.Name,

                PositionName = offer.Application.JobPosting.Position.Name,

                OfferedSalary = offer.OfferedSalary,

                OfferDate = offer.OfferDate,

                ExpiryDate = offer.ExpiryDate,

                ResponseDate = offer.ResponseDate,

                Status = offer.Status,

                Notes = offer.Notes,

                ApprovedByEmployeeId = offer.ApprovedByEmployeeId,

                ApprovedByEmployeeName = offer.ApprovedByEmployee?.FullName,

                HasContract = offer.Contract != null
            };

            return ResponseFactory.Success(response, "Application offer retrieved successfully.");
        }
    }
}

