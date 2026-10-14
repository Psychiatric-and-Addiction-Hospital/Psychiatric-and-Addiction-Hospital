using Application.Common.Interfaces.HR.LeaveType;
using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveType;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.LeaveType
{
    public class DeleteLeaveTypeService : IDeleteLeaveType
    {
        private readonly ILeaveTypeValidation _validation;
        private readonly AddIdentityDbContext _context;
        public DeleteLeaveTypeService(ILeaveTypeValidation validation, AddIdentityDbContext context)
        {
            _validation = validation;
            _context = context;
        }
        public async Task<BaseResponse<bool>> DeleteAsync(DeleteLeaveTypeRequest request, CancellationToken ct)
        {
            var validation = await _validation.ValidateDeleteAsync(request, ct);
            if (!validation.Success)
                return ResponseFactory.Fail<bool>(validation.Message, validation.Errors);

            var leaveType = validation.Data!;

            var hasRequests = await _context.LeaveRequests.AnyAsync(x => x.LeaveTypeId == leaveType.Id, ct);

            if (hasRequests)
                return ResponseFactory.Fail<bool>("Cannot deactivate leave type because it is already used.");

            leaveType.IsActive = false;

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(true, "Leave type deactivated successfully.");
        }
    }
}
