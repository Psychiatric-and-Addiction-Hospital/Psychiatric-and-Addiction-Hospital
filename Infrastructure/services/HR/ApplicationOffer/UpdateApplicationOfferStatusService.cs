using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.ApplicationOffer
{
    public class UpdateApplicationOfferStatusService : IUpdateApplicationOfferStatus
    {
        private readonly AddIdentityDbContext _Context;
        public UpdateApplicationOfferStatusService(AddIdentityDbContext Context)
        {
            _Context = Context;
        }
        public async Task<BaseResponse<ApplicationOfferResponse>> UpdateApplicationOfferStatusAsync(Guid OfferId, OfferStatues Status, CancellationToken ct)
        {
            var appoffer = await _Context.ApplicationOffers.FirstOrDefaultAsync(o => o.Id == OfferId, ct);
            if (appoffer is null)
                return ResponseFactory.Fail<ApplicationOfferResponse>("Application offer not found.");

            appoffer.statues = Status;

            await _Context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new ApplicationOfferResponse
            {
                Id = appoffer.Id,
                ApplicationProcessId = appoffer.ApplicationProcessId,
                OfferedSalary = appoffer.OfferedSalary,
                ExpiryDate = appoffer.ExpiryDate,
                Statues = appoffer.statues
            },
              "Offer status updated successfully");
        }
    }
}
