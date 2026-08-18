using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Contract;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.CandidatePortal
{
    public class SignContractService : ISignContract
    {
        private readonly AddIdentityDbContext _context;
        private readonly IContractValidation _validation;

        public SignContractService(AddIdentityDbContext context, IContractValidation validation)
        {
            _context = context;
            _validation = validation;
        }
        public async Task<BaseResponse<ContractResponse>> SignContractAsync(Guid ContractId, CancellationToken ct)
        {
            var validation = await _validation.ValidateSignAsync(ContractId, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<ContractResponse>(validation.Message, validation.Errors);

            var contract = validation.Data!;

            contract.Status = ContractStatus.Signed;

            contract.SignedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);

            var signedContract = await _context.Contracts
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
                Id = signedContract.Id,

                OfferId = signedContract.OfferId,

                ApplicationId = signedContract.Offer.ApplicationId,

                CandidateId = signedContract.Offer.Application.CandidateId,

                CandidateName = signedContract.Offer.Application.Candidate.FullName,

                JobPostingId = signedContract.Offer.Application.JobPostingId,

                JobTitle = signedContract.Offer.Application.JobPosting.Title,

                DepartmentName = signedContract.Offer.Application.JobPosting.Department.Name,

                PositionName = signedContract.Offer.Application.JobPosting.Position.Name,

                StartDate = signedContract.StartDate,

                EndDate = signedContract.EndDate,

                BaseSalary = signedContract.BaseSalary,

                SignedDate = signedContract.SignedDate,

                ProbationEndDate = signedContract.ProbationEndDate,

                ContractType = signedContract.ContractType,

                Status = signedContract.Status,

                Terms = signedContract.Terms
            };

            return ResponseFactory.Success(response, "Contract signed successfully.");
        }
    }
}
