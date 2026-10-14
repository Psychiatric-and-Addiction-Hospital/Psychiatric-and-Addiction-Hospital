using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Attendance;
using Infrastructure.Persistence.Identity;

namespace Infrastructure.services.HR.Attendance
{
    public class CheckOutAttendanceService : ICheckOutAttendance
    {
        private readonly AddIdentityDbContext _context;

        private readonly IAttendanceCalculator _calculator;

        private readonly IAttendanceValidation _validation;
        public CheckOutAttendanceService(
            AddIdentityDbContext context,
            IAttendanceCalculator calculator,
            IAttendanceValidation validation)
        {
            _context = context;
            _calculator = calculator;
            _validation = validation;
        }
        public async Task<BaseResponse<AttendanceResponse>> CheckOutAsync(string appUserId, string qrToken, CancellationToken ct)
        {
            var validationResponse = await _validation.ValidateCheckOutAsync(appUserId, qrToken, ct);

            if (!validationResponse.Success)
                return ResponseFactory.Fail<AttendanceResponse>(validationResponse.Message);

            var attendance = validationResponse.Data;
            var now = DateTime.UtcNow;

            var calculation = _calculator.Calculate(attendance.Employee, attendance.CheckInTime!.Value, now);
            attendance.CheckOutTime = now;

            attendance.ActualWorkedTime = calculation.WorkedTime;

            attendance.Overtime = calculation.Overtime;

            attendance.EarlyLeaveMinutes = calculation.EarlyLeaveMinutes;

            attendance.IsLocked = true;

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new AttendanceResponse
            {
                Id = attendance.Id,
                AttendanceDate = attendance.AttendanceDate,
                CheckInTime = attendance.CheckInTime!.Value,
                CheckOutTime = attendance.CheckOutTime.Value,
                AttendanceStatus = attendance.AttendanceStatus,
                LateMinutes = attendance.LateMinutes,
                WorkedTime = attendance.ActualWorkedTime,
                Overtime = attendance.Overtime,
            }, "Check-Out completed successfully.");
        }
    }
}
