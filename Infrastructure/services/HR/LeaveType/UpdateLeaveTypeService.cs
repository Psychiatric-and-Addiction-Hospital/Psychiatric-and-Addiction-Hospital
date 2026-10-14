using Application.Common.Interfaces.HR.LeaveType;
using Application.Common.Responses;
using Application.DTOS.Request.HR.LeaveType;
using Application.DTOS.Responses.HR.LeaveType;
using Infrastructure.Persistence.Identity;

namespace Infrastructure.services.HR.LeaveType
{
    public class UpdateLeaveTypeService : IUpdateLeaveType
    {
        private readonly ILeaveTypeValidation _validation;
        private readonly AddIdentityDbContext _context;
        public UpdateLeaveTypeService(ILeaveTypeValidation validation, AddIdentityDbContext context)
        {
            _validation = validation;
            _context = context;
        }
        public async Task<BaseResponse<LeaveTypeResponse>> UpdateAsync(UpdateLeaveTypeRequest request, CancellationToken ct)
        {
            var validation = await _validation.ValidateUpdateAsync(request, ct);

            if (!validation.Success)
                return ResponseFactory.Fail<LeaveTypeResponse>(validation.Message, validation.Errors);

            var leaveType = validation.Data!;

            leaveType.Name = request.Name.Trim();

            leaveType.Description = request.Description?.Trim();

            leaveType.MaxDaysPerYear = request.MaxDaysPerYear;

            leaveType.IsPaid = request.IsPaid;

            leaveType.RequiresApproval = request.RequiresApproval;

            leaveType.AllowHalfDay = request.AllowHalfDay;

            leaveType.IsActive = request.IsActive;

            await _context.SaveChangesAsync(ct);

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

            }, "Leave type updated successfully.");
        }
    }
}
