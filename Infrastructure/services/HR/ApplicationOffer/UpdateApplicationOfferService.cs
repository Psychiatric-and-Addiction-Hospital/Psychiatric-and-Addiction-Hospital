using Application.Commands.HR.ApplicationOffer;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Request.HR.ApplicationOffer;
using Application.DTOS.Responses.HR.ApplicationOffer;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.ApplicationOffer
{
    public class UpdateApplicationOfferService : IUpdateApplicationOffer
    {
        private readonly AddIdentityDbContext _context;
        private readonly IApplicationOfferValidation _validation;

        public UpdateApplicationOfferService(AddIdentityDbContext context, IApplicationOfferValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<ApplicationOfferResponse>> UpdateAsync(
            UpdateApplicationOfferRequest request,
            CancellationToken ct)
        {
            var validation = await _validation.ValidateUpdateAsync(request, ct);

            if (!validation.Success)
            {
                return ResponseFactory.Fail<ApplicationOfferResponse>(
                    validation.Message,
                    validation.Errors);
            }

            var offer = validation.Data!;

            //-------------------------------------------------
            // Update
            //-------------------------------------------------

            offer.OfferedSalary = request.OfferedSalary;

            offer.OfferDate = request.OfferDate;

            offer.ExpiryDate = request.ExpiryDate;

            offer.Notes = request.Notes?.Trim();

            offer.ApprovedByEmployeeId = request.ApprovedByEmployeeId;

            await _context.SaveChangesAsync(ct);

            //-------------------------------------------------
            // Reload
            //-------------------------------------------------

            var updatedOffer = await _context.ApplicationOffers
                .AsNoTracking()
                .Include(x => x.Application)
                    .ThenInclude(x => x.Candidate)
                .Include(x => x.Application)
                    .ThenInclude(x => x.JobPosting)
                .Include(x => x.ApprovedByEmployee)
                .FirstAsync(x => x.Id == offer.Id, ct);

            //-------------------------------------------------
            // Response
            //-------------------------------------------------

            var response = new ApplicationOfferResponse
            {
                Id = updatedOffer.Id,

                ApplicationId = updatedOffer.ApplicationId,

                CandidateId = updatedOffer.Application.CandidateId,

                CandidateName = updatedOffer.Application.Candidate.FullName,

                JobPostingId = updatedOffer.Application.JobPostingId,

                JobTitle = updatedOffer.Application.JobPosting.Title,

                OfferedSalary = updatedOffer.OfferedSalary,

                OfferDate = updatedOffer.OfferDate,

                ExpiryDate = updatedOffer.ExpiryDate,

                ResponseDate = updatedOffer.ResponseDate,

                Status = updatedOffer.Status,

                Notes = updatedOffer.Notes,

                ApprovedByEmployeeId =
                    updatedOffer.ApprovedByEmployeeId,

                ApprovedByEmployeeName =
                    updatedOffer.ApprovedByEmployee?.FullName
            };

            return ResponseFactory.Success(response, "Application offer updated successfully.");
        }
    }
}
