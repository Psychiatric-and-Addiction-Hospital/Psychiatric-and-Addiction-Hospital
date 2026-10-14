using Application.Commands.HR.Shift;
using Application.Common.Interfaces.HR.Shift;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Shift;
using Application.DTOS.Responses.HR.Shift;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.shift
{
    internal class UpdateShiftService : IUpdateShift
    {
        private readonly AddIdentityDbContext _context;

        public UpdateShiftService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<ShiftResponse>> UpdateAsync(
            UpdateShiftRequest request,
            CancellationToken ct)
        {
            var shift = await _context.Shifts
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (shift is null)
                return ResponseFactory.Fail<ShiftResponse>("Shift not found.");

            var exists = await _context.Shifts.AnyAsync(x =>
                x.Name == request.Name &&
                x.Id != request.Id, ct);

            if (exists)
                return ResponseFactory.Fail<ShiftResponse>("Shift name already exists.");

            shift.Name = request.Name.Trim();
            shift.StartTime = request.StartTime;
            shift.EndTime = request.EndTime;
            shift.BreakMinutes = request.BreakMinutes;
            shift.IsNightShift = request.IsNightShift;
            shift.ToleranceMinutes = request.ToleranceMinutes;
            shift.IsActive = request.IsActive;

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(
                new ShiftResponse
                {
                    Id = shift.Id,
                    Name = shift.Name,
                    StartTime = shift.StartTime,
                    EndTime = shift.EndTime,
                    BreakMinutes = shift.BreakMinutes,
                    IsNightShift = shift.IsNightShift,
                    ToleranceMinutes = shift.ToleranceMinutes,
                    IsActive = shift.IsActive
                },
                "Shift updated successfully.");
        }
    }
}

