using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Contract;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using ContractEntity = Domain.Entites.HR.Contract;
using Offer = Domain.Entites.HR.Recruitment.ApplicationOffer;

namespace Infrastructure.services.HR.Contract
{
    public class ContractValidationService : IContractValidation
    {
        private readonly AddIdentityDbContext _context;
        private readonly ICurrentUser _currentUser;
        public ContractValidationService(AddIdentityDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<bool>> ValidateCreateAsync(CreateContractRequest request, CancellationToken ct)
        {
            var offer = await GetOffer(request.OfferId, ct);

            if (offer == null)
                return ResponseFactory.Fail<bool>("Offer not found.");

            if (offer.Status != OfferStatus.Accepted)
                return ResponseFactory.Fail<bool>("Only accepted offers can have contracts.");

            if (offer.Application.Status != ApplicationStatus.Offered)
                return ResponseFactory.Fail<bool>("Application must be in Offered status.");

            if (offer.Contract != null)
                return ResponseFactory.Fail<bool>("This offer already has a contract.");

            var salaryValidation = ValidateSalary(request.BaseSalary);

            if (salaryValidation != null) return salaryValidation;

            var dateValidation = ValidateDates(request.StartDate, request.EndDate, request.ProbationEndDate);

            if (dateValidation != null)
                return dateValidation;

            return ResponseFactory.Success(true, "Validation succeeded.");
        }

        public async Task<BaseResponse<ContractEntity>> ValidateUpdateAsync(UpdateContractRequest request, CancellationToken ct)
        {
            var contract = await GetContract(request.Id, ct);

            if (contract == null)
                return ResponseFactory.Fail<ContractEntity>("Contract not found.");

            if (contract.Status != ContractStatus.Draft)
                return ResponseFactory.Fail<ContractEntity>("Only draft contracts can be updated.");

            var salaryValidation = ValidateSalary(request.BaseSalary);

            if (salaryValidation != null)
                return ResponseFactory.Fail<ContractEntity>(salaryValidation.Message);

            var dateValidation = ValidateDates(request.StartDate, request.EndDate, request.ProbationEndDate);

            if (dateValidation != null)
                return ResponseFactory.Fail<ContractEntity>(dateValidation.Message);

            return ResponseFactory.Success(contract, "Validation succeeded.");
        }

        public async Task<BaseResponse<ContractEntity>> ValidateSubmitAsync(Guid contractId, CancellationToken ct)
        {
            var contract = await GetContract(contractId, ct);

            if (contract == null)
                return ResponseFactory.Fail<ContractEntity>("Contract not found.");

            if (contract.Status != ContractStatus.Draft)
                return ResponseFactory.Fail<ContractEntity>("Only draft contracts can be submitted.");

            if (string.IsNullOrWhiteSpace(contract.Terms))
                return ResponseFactory.Fail<ContractEntity>("Contract terms are required before submission.");

            if (contract.BaseSalary <= 0)
                return ResponseFactory.Fail<ContractEntity>("Base salary must be greater than zero.");

            return ResponseFactory.Success(contract, "Validation succeeded.");
        }

        public async Task<BaseResponse<ContractEntity>> ValidateSignAsync(Guid contractId, CancellationToken ct)
        {
            var contract = await GetContract(contractId, ct);

            if (contract == null)
                return ResponseFactory.Fail<ContractEntity>("Contract not found.");

            if (contract.Status != ContractStatus.PendingSignature)
                return ResponseFactory.Fail<ContractEntity>("Contract is not awaiting signature.");

            if (contract.StartDate < DateTime.UtcNow.Date)
                return ResponseFactory.Fail<ContractEntity>("Cannot sign a contract whose start date has already passed.");

            if (!_currentUser.IsAuthenticated)
                return ResponseFactory.Fail<ContractEntity>("User must be authenticated to sign a contract.");

            var userId = _currentUser.UserId;

            if (string.IsNullOrWhiteSpace(userId))
                return ResponseFactory.Fail<ContractEntity>("Authenticated user must have a valid user ID.");

            var candidate = contract.Offer?.Application?.Candidate;

            if (candidate == null)
                return ResponseFactory.Fail<ContractEntity>("Candidate information was not found.");

            if (candidate.AppUserId != userId)
                return ResponseFactory.Fail<ContractEntity>("You are not authorized to sign this contract.");

            return ResponseFactory.Success(contract, "Validation succeeded.");
        }

        public async Task<BaseResponse<ContractEntity>> ValidateCancelAsync(Guid contractId, CancellationToken ct)
        {
            var contract = await GetContract(contractId, ct);

            if (contract == null)
                return ResponseFactory.Fail<ContractEntity>("Contract not found.");

            if (contract.Status == ContractStatus.Signed)
                return ResponseFactory.Fail<ContractEntity>("Signed contracts cannot be cancelled.");

            if (contract.Status == ContractStatus.Cancelled)
                return ResponseFactory.Fail<ContractEntity>("Contract is already cancelled.");

            if (contract.Status == ContractStatus.Expired)
                return ResponseFactory.Fail<ContractEntity>("Expired contracts cannot be cancelled.");

            return ResponseFactory.Success(contract, "Validation succeeded.");
        }

        //----------------------------------------------------
        // Helpers
        //----------------------------------------------------

        private async Task<Offer?> GetOffer(Guid id, CancellationToken ct)
        {
            return await _context.ApplicationOffers.Include(x => x.Contract).Include(x => x.Application).FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        private async Task<ContractEntity?> GetContract(Guid id, CancellationToken ct)
        {
            return await _context.Contracts
                .Include(x => x.Offer)
                    .ThenInclude(x => x.Application)
                        .ThenInclude(x => x.Candidate)
                .FirstOrDefaultAsync(
                    x => x.Id == id, ct);
        }

        private BaseResponse<bool>? ValidateSalary(decimal salary)
        {
            if (salary <= 0)
                return ResponseFactory.Fail<bool>("Base salary must be greater than zero.");

            return null;
        }

        private BaseResponse<bool>? ValidateDates(DateTime startDate, DateTime? endDate, DateTime? probationEndDate)
        {
            if (endDate.HasValue && endDate < startDate)
                return ResponseFactory.Fail<bool>("End date must be after start date.");

            if (probationEndDate.HasValue && probationEndDate < startDate)
            {
                return ResponseFactory.Fail<bool>("Probation end date must be after start date.");
            }

            return null;
        }
    }
}
