using Application.Common.Interfaces.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Contract;
using Application.DTOS.Responses.HR.Contract;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using ContractEntity = Domain.Entites.HR.Contract;


namespace Infrastructure.services.HR.Contract
{
    public class CreateContractService : ICreateContract
    {
        private readonly AddIdentityDbContext _context;
        private readonly IContractValidation _validation;

        public CreateContractService(AddIdentityDbContext context, IContractValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<ContractResponse>> CreateAsync(CreateContractRequest request, CancellationToken ct)
        {
            var validation = await _validation.ValidateCreateAsync(request, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<ContractResponse>(validation.Message, validation.Errors);


            var contract = new ContractEntity
            {
                OfferId = request.OfferId,

                StartDate = request.StartDate,

                EndDate = request.EndDate,

                BaseSalary = request.BaseSalary,

                ProbationEndDate = request.ProbationEndDate,

                ContractType = request.ContractType,

                Status = ContractStatus.Draft,

                Terms = request.Terms?.Trim(),

                SignedDate = null
            };

            _context.Contracts.Add(contract);

            await _context.SaveChangesAsync(ct);

            var createdContract = await _context.Contracts
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
                Id = createdContract.Id,

                OfferId = createdContract.OfferId,

                ApplicationId = createdContract.Offer.ApplicationId,

                CandidateId = createdContract.Offer.Application.CandidateId,

                CandidateName = createdContract.Offer.Application.Candidate.FullName,

                JobPostingId = createdContract.Offer.Application.JobPostingId,

                JobTitle = createdContract.Offer.Application.JobPosting.Title,

                DepartmentName = createdContract.Offer.Application.JobPosting.Department.Name,

                PositionName = createdContract.Offer.Application.JobPosting.Position.Name,

                StartDate = createdContract.StartDate,

                EndDate = createdContract.EndDate,

                BaseSalary = createdContract.BaseSalary,

                SignedDate = createdContract.SignedDate,

                ProbationEndDate = createdContract.ProbationEndDate,

                ContractType = createdContract.ContractType,

                Status = createdContract.Status,

                Terms = createdContract.Terms
            };

            return ResponseFactory.Success(response, "Contract created successfully.");
        }
    }
}
