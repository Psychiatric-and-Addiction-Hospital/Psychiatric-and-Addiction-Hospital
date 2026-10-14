using Application.Common.Interfaces.HR.LeaveType;
using Application.Common.Responses;
using Infrastructure.Persistence.Identity;

namespace Infrastructure.services.HR.LeaveType
{
    public class RestoreLeaveTypeService : IRestoreLeaveType
    {
        public readonly AddIdentityDbContext _context;
        public RestoreLeaveTypeService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<bool>> RestoreAsync(Guid Id, CancellationToken ct)
        {
            var leaveType = await _context.LeaveTypes.FindAsync(Id);
            if (leaveType == null)

                return ResponseFactory.Fail<bool>("Leave type not found");

            leaveType.IsActive = true;

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(true, "Leave type restored successfully.");
        }
    }
}
