using Application.Commands.HR.ApplicationOffer;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Common;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.ApplicationOffer;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.CandidatePortal
{
    public class AcceptApplicationOfferService : IAcceptApplicationOffer
    {
        private readonly AddIdentityDbContext _context;
        private readonly IApplicationOfferValidation _validation;

        public AcceptApplicationOfferService(AddIdentityDbContext context, IApplicationOfferValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<ApplicationOfferResponse>> AcceptAsync(Guid OfferId, CancellationToken ct)
        {
            var validation = await _validation.ValidateAcceptAsync(OfferId, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<ApplicationOfferResponse>(validation.Message, validation.Errors);

            var offer = validation.Data!;


            offer.Status = OfferStatus.Accepted;

            offer.ResponseDate = DateTime.UtcNow;

            var application = await _context.Applications.FirstAsync(x => x.Id == offer.ApplicationId, ct);

            application.Status = ApplicationStatus.Offered;

            await _context.SaveChangesAsync(ct);

            var acceptedOffer = await _context.ApplicationOffers
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
                Id = acceptedOffer.Id,
                ApplicationId = acceptedOffer.ApplicationId,
                CandidateId = acceptedOffer.Application.CandidateId,
                CandidateName = acceptedOffer.Application.Candidate.FullName,
                JobPostingId = acceptedOffer.Application.JobPostingId,
                JobTitle = acceptedOffer.Application.JobPosting.Title,
                DepartmentName = acceptedOffer.Application.JobPosting.Department.Name,
                PositionName = acceptedOffer.Application.JobPosting.Position.Name,
                OfferedSalary = acceptedOffer.OfferedSalary,
                OfferDate = acceptedOffer.OfferDate,
                ExpiryDate = acceptedOffer.ExpiryDate,
                ResponseDate = acceptedOffer.ResponseDate,
                Status = acceptedOffer.Status,
                Notes = acceptedOffer.Notes,
                ApprovedByEmployeeId = acceptedOffer.ApprovedByEmployeeId,
                ApprovedByEmployeeName = acceptedOffer.ApprovedByEmployee?.FullName,
                HasContract = acceptedOffer.Contract != null
            };

            return ResponseFactory.Success(response, "Application offer accepted successfully.");
        }
    }
}