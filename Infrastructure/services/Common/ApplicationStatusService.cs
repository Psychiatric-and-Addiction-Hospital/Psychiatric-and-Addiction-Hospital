using Application.Common.Interfaces.Common;
using Application.Common.Responses;
using Domain.Entites.HR.Recruitment;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using EntityApplication = Domain.Entites.HR.Recruitment.Application;

namespace Infrastructure.services.Common
{
    public class ApplicationStatusService : IApplicationStatusService
    {
        private readonly AddIdentityDbContext _context;

        public ApplicationStatusService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<EntityApplication>> ChangeStatusAsync(Guid applicationId, ApplicationStatus newStatus, string? notes, CancellationToken ct)
        {
            var application = await _context.Applications
        .FirstOrDefaultAsync(x => x.Id == applicationId, ct);

            if (application == null)
                return ResponseFactory.Fail<EntityApplication>("Application not found.");

            if (application.Status == newStatus)
                return ResponseFactory.Fail<EntityApplication>("Application already has this status.");

            application.Status = newStatus;

            var history = new ApplicationStatusHistory
            {
                ApplicationId = application.Id,
                Status = newStatus,
                ChangedAt = DateTime.UtcNow,
                Notes = notes
            };

            await _context.ApplicationStatusHistorys.AddAsync(history, ct);


            return ResponseFactory.Success(application, "Application status updated successfully.");
        }
    }
}
