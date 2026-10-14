using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Domain.Entites.HR;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using VaildAttendance = Domain.Entites.HR.Attendance;
using employeeEntity = Domain.Entites.HR.Employee;

namespace Infrastructure.services.HR.Attendance
{
    public class AttendanceValidationService : IAttendanceValidation
    {
        private readonly AddIdentityDbContext _context;
        private readonly IAttendanceToken _token;

        public AttendanceValidationService(
            AddIdentityDbContext context,
            IAttendanceToken token)
        {
            _context = context;
            _token = token;
        }

        #region ValidateCheckInAsync
        public async Task<BaseResponse<employeeEntity>> ValidateCheckInAsync(string appUserId, string qrToken, CancellationToken ct)
        {
            if (!_token.TryValidateToken(qrToken, out _))
                return ResponseFactory.Fail<employeeEntity>("QR Code expired.");

            var employee = await _context.Employees.
                Include(e => e.Shift).
                FirstOrDefaultAsync(x => x.AppUserId == appUserId, ct);

            if (employee == null)
                return ResponseFactory.Fail<employeeEntity>("Employee not found.");

            if (!employee.IsActive)
                return ResponseFactory.Fail<employeeEntity>("Employee inactive.");

            if (employee.Shift == null)
                return ResponseFactory.Fail<employeeEntity>("Shift not assigned.");

            if (!employee.Shift.IsActive)
                return ResponseFactory.Fail<employeeEntity>("Shift inactive.");

            if (employee.EmploymentStatus != EmploymentStatus.Active)
                return ResponseFactory.Fail<employeeEntity>("Employee is not currently active.");


            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var exists = await _context.Attendances.
                AnyAsync(
                x => x.EmployeeId == employee.Id
                && x.AttendanceDate == today, ct);

            if (exists)
                return ResponseFactory.Fail<employeeEntity>("Attendance already exists.");

            return ResponseFactory.Success(employee, "Validation succeeded.");

        }

        #endregion

        #region ValidateCheckOutAsync
        public async Task<BaseResponse<VaildAttendance>> ValidateCheckOutAsync(string appUserId, string qrToken, CancellationToken ct)
        {
            if (!_token.TryValidateToken(qrToken, out _))
                return ResponseFactory.Fail<VaildAttendance>("QR Code expired.");

            var employee = await _context.Employees.
               Include(e => e.Shift).
               FirstOrDefaultAsync(x => x.AppUserId == appUserId, ct);

            if (employee == null)
                return ResponseFactory.Fail<VaildAttendance>("Employee not found.");

            if (!employee.IsActive)
                return ResponseFactory.Fail<VaildAttendance>("Employee inactive.");

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var attendance = await _context.Attendances
                .Include(a => a.Employee)
                .ThenInclude(e => e.Shift)
                .FirstOrDefaultAsync(
                x => x.Employee.AppUserId == appUserId
                && x.AttendanceDate == today, ct);

            if (attendance == null)
                return ResponseFactory.Fail<VaildAttendance>("Check-In record not found.");

            if (attendance.CheckOutTime != null)
                return ResponseFactory.Fail<VaildAttendance>("Employee already checked out.");

            if (attendance.IsLocked)
                return ResponseFactory.Fail<VaildAttendance>("Attendance record is locked.");

            return ResponseFactory.Success(attendance, "Validation succeeded.");
        }
        #endregion
    }
}