using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.ApplicationOffer
{
    public class RejectApplicationOfferService : IRejectApplicationOffer
    {
        private readonly AddIdentityDbContext _context;
        private readonly IApplicationOfferValidation _validation;

        public RejectApplicationOfferService(AddIdentityDbContext context, IApplicationOfferValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<ApplicationOfferResponse>> RejectAsync(
             Guid OfferId,
            CancellationToken ct)
        {
            var validation = await _validation.ValidateRejectAsync(OfferId, ct);

            if (!validation.Success)
            {
                return ResponseFactory.Fail<ApplicationOfferResponse>(
                    validation.Message,
                    validation.Errors);
            }

            var offer = validation.Data!;

            // Reject Offer

            offer.Status = OfferStatus.Rejected;

            offer.ResponseDate = DateTime.UtcNow;

            // Update Application

            var application = await _context.Applications
                .FirstAsync(x => x.Id == offer.ApplicationId, ct);

            application.Status = ApplicationStatus.OfferDeclined;

            await _context.SaveChangesAsync(ct);

            // Reload                

            var rejectedOffer = await _context.ApplicationOffers
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
                .FirstAsync(x => x.Id == offer.Id, ct);

            var response = new ApplicationOfferResponse
            {
                Id = rejectedOffer.Id,
                ApplicationId = rejectedOffer.ApplicationId,
                CandidateId = rejectedOffer.Application.CandidateId,
                CandidateName = rejectedOffer.Application.Candidate.FullName,
                JobPostingId = rejectedOffer.Application.JobPostingId,
                JobTitle = rejectedOffer.Application.JobPosting.Title,
                DepartmentName = rejectedOffer.Application.JobPosting.Department.Name,
                PositionName = rejectedOffer.Application.JobPosting.Position.Name,
                OfferedSalary = rejectedOffer.OfferedSalary,
                OfferDate = rejectedOffer.OfferDate,
                ExpiryDate = rejectedOffer.ExpiryDate,
                ResponseDate = rejectedOffer.ResponseDate,
                Status = rejectedOffer.Status,
                Notes = rejectedOffer.Notes,
                ApprovedByEmployeeId = rejectedOffer.ApprovedByEmployeeId,
                ApprovedByEmployeeName = rejectedOffer.ApprovedByEmployee?.FullName,
                HasContract = rejectedOffer.Contract != null
            };

            return ResponseFactory.Success(response, "Application offer rejected successfully.");
        }
    }
}

