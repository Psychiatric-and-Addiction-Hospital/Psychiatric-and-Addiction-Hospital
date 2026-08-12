using Application.Common.Interfaces.HR.LeaveType;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.LeaveType;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.LeaveType
{
    public class GetleaveTypeByIdService : IGetleaveTypeById
    {
        private readonly AddIdentityDbContext _context;
        public GetleaveTypeByIdService(AddIdentityDbContext context)
        {
            _context = context;
        }
        public async Task<BaseResponse<LeaveTypeResponse>> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var leaveType = await _context.LeaveTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, ct);

            if (leaveType == null)
                return ResponseFactory.Fail<LeaveTypeResponse>("Leave type not found.");


            return ResponseFactory.Success(new LeaveTypeResponse
            {
                Id = leaveType.Id,
                Name = leaveType.Name,
                Description = leaveType.Description,
                MaxDaysPerYear = leaveType.MaxDaysPerYear,
                IsPaid = leaveType.IsPaid,
                RequiresApproval = leaveType.RequiresApproval,
                AllowHalfDay = leaveType.AllowHalfDay,
                IsActive = leaveType.IsActive
            }, "Leave type retrieved successfully.");
        }
    }
}
