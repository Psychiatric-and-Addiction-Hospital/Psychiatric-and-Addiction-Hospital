using Application.Commands.HR.Shift;
using Application.Common.Interfaces.HR.Shift;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Shift;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.shift
{
    public class DeleteShiftService: IDeleteShift
    {
        private readonly AddIdentityDbContext _context;

        public DeleteShiftService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<ShiftResponse>> DeleteAsync(
            DeleteShiftCommand request,
            CancellationToken ct)
        {
            var shift = await _context.Shifts
                .Include(s => s.Employees)
                .Include(s => s.Attendances)
                .FirstOrDefaultAsync(s => s.Id == request.Id, ct);

            if (shift is null)
                return ResponseFactory.Fail<ShiftResponse>("Shift not found.");

            if (shift.Employees.Any())
                return ResponseFactory.Fail<ShiftResponse>(
                    "Cannot delete shift because it is assigned to employees.");

            if (shift.Attendances.Any())
                return ResponseFactory.Fail<ShiftResponse>(
                    "Cannot delete shift because attendance records exist.");

            _context.Shifts.Remove(shift);

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(
                new ShiftResponse
                {
                    Id = shift.Id,
                    Name = shift.Name
                }, "Shift deleted successfully.");
        }
    }
}
