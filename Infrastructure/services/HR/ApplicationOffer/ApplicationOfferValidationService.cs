using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationOffer;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using ApplicationEntity = Domain.Entites.HR.Recruitment.Application;
using Offer = Domain.Entites.HR.Recruitment.ApplicationOffer;

namespace Infrastructure.services.HR.ApplicationOffer
{
    public class ApplicationOfferValidationService : IApplicationOfferValidation
    {
        private readonly AddIdentityDbContext _context;

        private readonly ICurrentUser _currentUser;
        public ApplicationOfferValidationService(AddIdentityDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<bool>> ValidateCreateAsync(CreateApplicationOfferRequest request, CancellationToken ct)
        {
            var application = await GetApplication(request.ApplicationId, ct);

            if (application == null)
                return ResponseFactory.Fail<bool>("Application not found.");

            if (application.Offer != null)
                return ResponseFactory.Fail<bool>("This application already has an offer.");

            if (application.Status != ApplicationStatus.InterviewCompleted)
                return ResponseFactory.Fail<bool>("Interview must be completed before creating an offer.");


            var lastInterview = await _context.ApplicationInterviews
                .Where(x => x.ApplicationId == request.ApplicationId)
                .OrderByDescending(x => x.ScheduledAt)
                .FirstOrDefaultAsync(ct);

            if (lastInterview == null)
                return ResponseFactory.Fail<bool>("Interview not found.");

            if (lastInterview.Result != InterviewResult.Passed)
                return ResponseFactory.Fail<bool>(
                    "Candidate did not pass the interview.");


            if (request.ApprovedByEmployeeId.HasValue)
            {
                var employee = await GetEmployee(
                    request.ApprovedByEmployeeId.Value,
                    ct);

                if (employee == null)
                    return ResponseFactory.Fail<bool>("Approver not found.");

                if (!employee.IsActive)
                    return ResponseFactory.Fail<bool>("Approver is inactive.");
            }

            var salaryValidation = ValidateSalary(request.OfferedSalary);

            if (salaryValidation != null)
                return salaryValidation;

            var dateValidation =
                ValidateDates(request.OfferDate, request.ExpiryDate);

            if (dateValidation != null)
                return dateValidation;

            return ResponseFactory.Success(true, "Validation succeeded.");
        }

        public async Task<BaseResponse<Offer>> ValidateUpdateAsync(UpdateApplicationOfferRequest request, CancellationToken ct)
        {
            var offer = await GetOffer(request.Id, ct);

            if (offer == null)
                return ResponseFactory.Fail<Offer>(
                    "Offer not found.");

            if (offer.Status == OfferStatus.Accepted)
                return ResponseFactory.Fail<Offer>(
                    "Accepted offers cannot be updated.");

            if (offer.Status == OfferStatus.Rejected)
                return ResponseFactory.Fail<Offer>(
                    "Rejected offers cannot be updated.");

            if (offer.ExpiryDate < DateTime.UtcNow)
                return ResponseFactory.Fail<Offer>("Expired offers cannot be updated.");


            if (request.ApprovedByEmployeeId.HasValue)
            {
                var employee = await GetEmployee(request.ApprovedByEmployeeId.Value, ct);

                if (employee == null)
                    return ResponseFactory.Fail<Offer>(
                        "Approver not found.");

                if (!employee.IsActive)
                    return ResponseFactory.Fail<Offer>(
                        "Approver is inactive.");
            }

            var salaryValidation =
                ValidateSalary(request.OfferedSalary);

            if (salaryValidation != null)
                return ResponseFactory.Fail<Offer>(
                    salaryValidation.Message);

            var dateValidation =
                ValidateDates(
                    request.OfferDate,
                    request.ExpiryDate);

            if (dateValidation != null)
                return ResponseFactory.Fail<Offer>(
                    dateValidation.Message);

            return ResponseFactory.Success(
                offer,
                "Validation succeeded.");
        }

        public async Task<BaseResponse<Offer>> ValidateAcceptAsync(Guid offerId, CancellationToken ct)
        {
            var offer = await GetOffer(offerId, ct);

            if (offer == null)
                return ResponseFactory.Fail<Offer>(
                    "Offer not found.");

            if (offer.Status != OfferStatus.Pending)
                return ResponseFactory.Fail<Offer>(
                    "Only pending offers can be accepted.");

            if (offer.ExpiryDate < DateTime.UtcNow)
                return ResponseFactory.Fail<Offer>(
                    "Offer has expired.");

            if (offer.Contract != null)
                return ResponseFactory.Fail<Offer>("Contract already exists.");

            if (!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<Offer>("User is not authenticated.");

            var currentUserId = _currentUser.UserId;

            if (string.IsNullOrWhiteSpace(currentUserId))
                return ResponseFactory.Fail<Offer>("User is not authenticated.");

            var candidate = await _context.Candidates
                .FirstOrDefaultAsync(
                    x => x.AppUserId == currentUserId, ct);

            if (candidate == null)
                return ResponseFactory.Fail<Offer>("Candidate profile was not found.");

            if (offer.Application.CandidateId != candidate.Id)
                return ResponseFactory.Fail<Offer>("You are not authorized to respond to this offer.");

            return ResponseFactory.Success(offer, "Validation succeeded.");
        }

        public async Task<BaseResponse<Offer>> ValidateRejectAsync(Guid offerId, CancellationToken ct)
        {
            var offer = await GetOffer(offerId, ct);

            if (offer == null)
                return ResponseFactory.Fail<Offer>(
                    "Offer not found.");

            if (offer.Status != OfferStatus.Pending)
                return ResponseFactory.Fail<Offer>("Only pending offers can be rejected.");

            if (!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<Offer>("User is not authenticated.");

            var currentUserId = _currentUser.UserId;

            if (string.IsNullOrWhiteSpace(currentUserId))
                return ResponseFactory.Fail<Offer>("User is not authenticated.");

            var candidate = await _context.Candidates
                .FirstOrDefaultAsync(
                    x => x.AppUserId == currentUserId, ct);

            if (candidate == null)
                return ResponseFactory.Fail<Offer>("Candidate profile was not found.");

            if (offer.Application.CandidateId != candidate.Id)
                return ResponseFactory.Fail<Offer>("You are not authorized to respond to this offer.");

            return ResponseFactory.Success(offer, "Validation succeeded.");
        }

        public async Task<BaseResponse<Offer>> ValidateDeleteAsync(Guid offerId, CancellationToken ct)
        {
            var offer = await GetOffer(offerId, ct);

            if (offer == null)
                return ResponseFactory.Fail<Offer>(
                    "Offer not found.");

            if (offer.Status == OfferStatus.Accepted)
                return ResponseFactory.Fail<Offer>(
                    "Accepted offers cannot be deleted.");

            if (offer.Contract != null)
                return ResponseFactory.Fail<Offer>(
                    "Offer has a contract.");

            if (offer.Status == OfferStatus.Pending)
                return ResponseFactory.Fail<Offer>("Pending offers cannot be deleted.");

            return ResponseFactory.Success(offer, "Validation succeeded.");
        }

        //----------------------------------------------------
        // Helpers
        //----------------------------------------------------

        private async Task<ApplicationEntity?>
            GetApplication(Guid id, CancellationToken ct)
        {
            return await _context.Applications
                .Include(x => x.Offer)
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        private async Task<Offer?> GetOffer(Guid id, CancellationToken ct)
        {
            return await _context.ApplicationOffers
                .Include(x => x.Contract)
                .Include(x => x.Application)
                     .ThenInclude(x => x.Candidate)
                     .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        private async Task<Domain.Entites.HR.Employee?> GetEmployee(Guid id, CancellationToken ct)
        {
            return await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        private BaseResponse<bool>? ValidateSalary(decimal salary)
        {
            if (salary <= 0)
                return ResponseFactory.Fail<bool>("Offered salary must be greater than zero.");

            return null;
        }

        private BaseResponse<bool>? ValidateDates(DateTime offerDate, DateTime expiryDate)
        {
            if (expiryDate < offerDate)
                return ResponseFactory.Fail<bool>("Expiry date must be after offer date.");

            return null;
        }
    }
}
