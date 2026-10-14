using Application.Common.Interfaces.HR.Application;
using Application.Common.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Application
{
    public class DeleteApplicationService : IDeleteApplication
    {
        private readonly AddIdentityDbContext _context;

        public DeleteApplicationService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid applicationId, CancellationToken ct)
        {
            var application = await _context.Applications
                .Include(x => x.Interviews)
                .Include(x => x.Offer)
                .FirstOrDefaultAsync(x => x.Id == applicationId, ct);

            if (application == null)
                return ResponseFactory.Fail<bool>("Application not found.");

            _context.Applications.Remove(application);

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(
                true,
                "Application deleted successfully.");
        }
    }
}
