using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationOffer;
using Application.DTOS.Responses.HR.ApplicationOffer;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using Offer = Domain.Entites.HR.Recruitment.ApplicationOffer;

namespace Infrastructure.services.HR.ApplicationOffer
{
    public class CreateApplicationOfferService : ICreateApplicationOffer
    {
        private readonly AddIdentityDbContext _context;
        private readonly IApplicationOfferValidation _validation;
        private readonly IJobOfferEmailService _emailService;
        public CreateApplicationOfferService(AddIdentityDbContext context, IApplicationOfferValidation validation, IJobOfferEmailService emailService)
        {
            _context = context;
            _validation = validation;
            _emailService = emailService;
        }
        public async Task<BaseResponse<ApplicationOfferResponse>> CreateAsync(CreateApplicationOfferRequest request, CancellationToken ct)
        {
            var validation = await _validation.ValidateCreateAsync(request, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<ApplicationOfferResponse>(validation.Message, validation.Errors);


            var offer = new Offer
            {
                ApplicationId = request.ApplicationId,

                OfferedSalary = request.OfferedSalary,

                OfferDate = request.OfferDate,

                ExpiryDate = request.ExpiryDate,

                Status = OfferStatus.Pending,

                Notes = request.Notes?.Trim(),

                ApprovedByEmployeeId = request.ApprovedByEmployeeId
            };

            _context.ApplicationOffers.Add(offer);


            var application = await _context.Applications
                .FirstAsync(x => x.Id == request.ApplicationId, ct);

            application.Status = ApplicationStatus.Offered;

            await _context.SaveChangesAsync(ct);

            var createdOffer = await _context.ApplicationOffers
                .AsNoTracking()
                .Include(x => x.Application)
                    .ThenInclude(a => a.Candidate)
                .Include(x => x.Application)
                    .ThenInclude(a => a.JobPosting)
                        .ThenInclude(j => j.Position)
                .Include(x => x.Application)
                    .ThenInclude(a => a.JobPosting)
                        .ThenInclude(j => j.Department)
                .Include(x => x.ApprovedByEmployee)
                .FirstAsync(x => x.Id == offer.Id, ct);

            await _emailService.SendAsync(createdOffer.Application.Candidate, createdOffer, ct);

            var response = new ApplicationOfferResponse
            {
                Id = createdOffer.Id,

                ApplicationId = createdOffer.ApplicationId,

                CandidateId = createdOffer.Application.CandidateId,

                CandidateName = createdOffer.Application.Candidate.FullName,

                JobPostingId = createdOffer.Application.JobPostingId,

                JobTitle = createdOffer.Application.JobPosting.Title,

                OfferedSalary = createdOffer.OfferedSalary,

                OfferDate = createdOffer.OfferDate,

                ExpiryDate = createdOffer.ExpiryDate,

                ResponseDate = createdOffer.ResponseDate,

                Status = createdOffer.Status,

                Notes = createdOffer.Notes,

                ApprovedByEmployeeId = createdOffer.ApprovedByEmployeeId,

                ApprovedByEmployeeName =
                    createdOffer.ApprovedByEmployee?.FullName
            };

            return ResponseFactory.Success(response, "Application offer created successfully.");
        }
    }

}

