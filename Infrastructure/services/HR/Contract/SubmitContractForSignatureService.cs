using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Contract;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Contract
{
    public class SubmitContractForSignatureService : ISubmitContractForSignature
    {
        private readonly AddIdentityDbContext _context;
        private readonly IContractValidation _validation;
        public SubmitContractForSignatureService(AddIdentityDbContext context, IContractValidation validation)
        {
            _context = context;
            _validation = validation;
        }
        public async Task<BaseResponse<ContractResponse>> SubmitContractForSignatureAsync(Guid ContractId, CancellationToken ct)
        {
            var validation = await _validation.ValidateSubmitAsync(ContractId, ct);
            if (!validation.Success)
                return ResponseFactory.Fail<ContractResponse>(validation.Message, validation.Errors);

            var contract = validation.Data!;

            contract.Status = ContractStatus.PendingSignature;

            await _context.SaveChangesAsync(ct);

            var SubmitContractForSignature = await _context.Contracts
                .AsNoTracking()
                .Include(x => x.Offer)
                .ThenInclude(x => x.Application)
                .ThenInclude(x => x.Candidate)
                .Include(x => x.Offer)
                .ThenInclude(x => x.Application)
                .ThenInclude(x => x.JobPosting)
                .ThenInclude(x => x.Department)
                .Include(x => x.Offer)
                .ThenInclude(x => x.Application)
                .ThenInclude(x => x.JobPosting)
                .ThenInclude(x => x.Position)
                .FirstAsync(x => x.Id == contract.Id, ct);

            var response = new ContractResponse
            {
                Id = SubmitContractForSignature.Id,

                OfferId = SubmitContractForSignature.OfferId,

                ApplicationId = SubmitContractForSignature.Offer.ApplicationId,

                CandidateId = SubmitContractForSignature.Offer.Application.CandidateId,

                CandidateName = SubmitContractForSignature.Offer.Application.Candidate.FullName,

                JobPostingId = SubmitContractForSignature.Offer.Application.JobPostingId,

                JobTitle = SubmitContractForSignature.Offer.Application.JobPosting.Title,

                DepartmentName = SubmitContractForSignature.Offer.Application.JobPosting.Department.Name,

                PositionName = SubmitContractForSignature.Offer.Application.JobPosting.Position.Name,

                StartDate = SubmitContractForSignature.StartDate,

                EndDate = SubmitContractForSignature.EndDate,

                BaseSalary = SubmitContractForSignature.BaseSalary,

                SignedDate = SubmitContractForSignature.SignedDate,

                ProbationEndDate = SubmitContractForSignature.ProbationEndDate,

                ContractType = SubmitContractForSignature.ContractType,

                Status = SubmitContractForSignature.Status,

                Terms = SubmitContractForSignature.Terms
            };

            return ResponseFactory.Success(response, "Contract submitted for signature successfully.");
        }
    }
}
