using Application.Commands.HR.ApplicationOffer;
using Application.Common.Interfaces.Common;
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
        private readonly IApplicationStatusService _statusService;

        public DeleteApplicationOfferService(AddIdentityDbContext context,
            IApplicationOfferValidation validation,
            IApplicationStatusService statusService)
        {
            _context = context;
            _validation = validation;
            _statusService = statusService;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid OfferId, CancellationToken ct)
        {
            var validation = await _validation.ValidateDeleteAsync(OfferId, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<bool>(validation.Message, validation.Errors);

            var offer = validation.Data!;

            var statusResult = await _statusService.ChangeStatusAsync(
                offer.ApplicationId,
                ApplicationStatus.InterviewCompleted,
                "Application offer was deleted.",
                ct);

            if (!statusResult.Success)
                return ResponseFactory.Fail<bool>(statusResult.Message, statusResult.Errors);

            _context.ApplicationOffers.Remove(offer);

            var application = await _context.Applications
                .FindAsync(new object[] { offer.ApplicationId }, ct);

            if (application != null)
                application.Status = ApplicationStatus.InterviewCompleted;

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(true, "Application offer deleted successfully.");
        }
    }
}
