using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Contract;
using Application.DTOS.Responses.HR.Contract;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Contract
{
    public class UpdateContractService : IUpdateContract
    {
        private readonly AddIdentityDbContext _context;
        private readonly IContractValidation _validation;

        public UpdateContractService(AddIdentityDbContext context, IContractValidation validation)
        {
            _context = context;
            _validation = validation;
        }
        public async Task<BaseResponse<ContractResponse>> UpdateAsync(UpdateContractRequest request, CancellationToken ct)
        {
            var validation = await _validation.ValidateUpdateAsync(request, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<ContractResponse>(validation.Message, validation.Errors);

            var contract = validation.Data!;

            contract.StartDate = request.StartDate;

            contract.EndDate = request.EndDate;

            contract.BaseSalary = request.BaseSalary;

            contract.ProbationEndDate = request.ProbationEndDate;

            contract.ContractType = request.ContractType;

            contract.Terms = request.Terms?.Trim();

            await _context.SaveChangesAsync(ct);

            var updateContract = await _context.Contracts.AsNoTracking()
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

                 .FirstOrDefaultAsync(c => c.Id == request.Id, ct);

            var response = new ContractResponse
            {
                Id = updateContract.Id,

                OfferId = updateContract.OfferId,

                ApplicationId = updateContract.Offer.ApplicationId,

                CandidateId = updateContract.Offer.Application.CandidateId,

                CandidateName = updateContract.Offer.Application.Candidate.FullName,

                JobPostingId = updateContract.Offer.Application.JobPostingId,

                JobTitle = updateContract.Offer.Application.JobPosting.Title,

                DepartmentName = updateContract.Offer.Application.JobPosting.Department.Name,

                PositionName = updateContract.Offer.Application.JobPosting.Position.Name,

                StartDate = updateContract.StartDate,

                EndDate = updateContract.EndDate,

                BaseSalary = updateContract.BaseSalary,

                SignedDate = updateContract.SignedDate,

                ProbationEndDate = updateContract.ProbationEndDate,

                ContractType = updateContract.ContractType,

                Status = updateContract.Status,

                Terms = updateContract.Terms
            };

            return ResponseFactory.Success(response, "Contract Update successfully.");
        }
    }
}
