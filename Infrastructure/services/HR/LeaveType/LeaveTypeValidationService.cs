using Application.Common.Interfaces.HR.LeaveType;
using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveType;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using leaveType = Domain.Entites.HR.Leave.LeaveType;

namespace Infrastructure.services.HR.LeaveType
{
    public class LeaveTypeValidationService : ILeaveTypeValidation
    {
        private readonly AddIdentityDbContext _context;

        public LeaveTypeValidationService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<bool>> ValidateCreateAsync(CreateLeaveTypeRequest request, CancellationToken ct)
        {
            var exists = await _context.LeaveTypes.AnyAsync(x => x.Name == request.Name.Trim(), ct);

            if (exists)
                return ResponseFactory.Fail<bool>("Leave type already exists.");

            return ResponseFactory.Success(true);
        }

        public async Task<BaseResponse<leaveType>> ValidateUpdateAsync(UpdateLeaveTypeRequest request, CancellationToken ct)
        {
            var leaveType = await _context.LeaveTypes
                .FirstOrDefaultAsync(x => x.Id == request.LeaveTypeId, ct);

            if (leaveType == null)
                return ResponseFactory.Fail<leaveType>("Leave type not found.");

            var exists = await _context.LeaveTypes.AnyAsync(
                x => x.Name == request.Name.Trim()
                && x.Id != request.LeaveTypeId, ct);

            if (exists)
                return ResponseFactory.Fail<leaveType>("Leave type name already exists.");

            return ResponseFactory.Success(leaveType);
        }

        public async Task<BaseResponse<leaveType>> ValidateDeleteAsync(DeleteLeaveTypeRequest request,CancellationToken ct)
        {
            var leaveType = await _context.LeaveTypes
                .FirstOrDefaultAsync(x => x.Id == request.LeaveTypeId, ct);

            if (leaveType == null)
                return ResponseFactory.Fail<leaveType>("Leave type not found.");

            if (!leaveType.IsActive)
                return ResponseFactory.Fail<leaveType>("Leave type is already inactive.");

            return ResponseFactory.Success(leaveType);
        }

    }
}
