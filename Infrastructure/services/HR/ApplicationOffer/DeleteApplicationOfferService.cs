using Application.Commands.HR.ApplicationOffer;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Responses;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;

namespace Infrastructure.services.HR.ApplicationOffer
{
    public class DeleteApplicationOfferService : IDeleteApplicationOffer
    {
        private readonly AddIdentityDbContext _context;
        private readonly IApplicationOfferValidation _validation;

        public DeleteApplicationOfferService(AddIdentityDbContext context, IApplicationOfferValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid OfferId, CancellationToken ct)
        {
            var validation = await _validation.ValidateDeleteAsync(OfferId, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<bool>(validation.Message, validation.Errors);

            var offer = validation.Data!;

            // Remove Offer            

            _context.ApplicationOffers.Remove(offer);

            // Restore Application Status

            var application = await _context.Applications
                .FindAsync(new object[] { offer.ApplicationId }, ct);

            if (application != null)
                application.Status = ApplicationStatus.InterviewCompleted;

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(true, "Application offer deleted successfully.");
        }
    }
}
