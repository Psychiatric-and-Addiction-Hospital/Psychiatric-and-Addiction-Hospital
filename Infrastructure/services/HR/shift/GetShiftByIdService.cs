using Application.Common.Interfaces.HR.Shift;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Shift;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.shift
{
    public class GetShiftByIdService : IGetShiftById
    {
        private readonly AddIdentityDbContext _context;

        public GetShiftByIdService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<ShiftResponse>> GetByIdAsync(
            Guid id,
            CancellationToken ct)
        {
            var shift = await _context.Shifts
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id, ct);

            if (shift is null)
                return ResponseFactory.Fail<ShiftResponse>("Shift not found.");

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
                "Shift retrieved successfully.");
        }
    }
}
