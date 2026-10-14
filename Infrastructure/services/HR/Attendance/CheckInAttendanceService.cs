using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Attendance;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;

namespace Infrastructure.services.HR.Attendance
{
    public class CheckInAttendanceService : ICheckInAttendance
    {
        private readonly AddIdentityDbContext _context;
        private readonly IAttendanceCalculator _calculator;
        private readonly IAttendanceValidation _validation;
        public CheckInAttendanceService(AddIdentityDbContext context, IAttendanceCalculator calculator, IAttendanceValidation validation)
        {
            _context = context;
            _calculator = calculator;
            _validation = validation;
        }

        public async Task<BaseResponse<AttendanceResponse>> CheckInAsync(string appUserId, string qrToken, CancellationToken ct)
        {
            var now = DateTime.UtcNow;

            var validation = await _validation.ValidateCheckInAsync(appUserId,qrToken,ct);

            if (!validation.Success)
            {
                return ResponseFactory.Fail<AttendanceResponse>(
                    validation.Message);
            }

            var employee = validation.Data!;

            var calculation = _calculator.Calculate(employee, now, null);
            var newAttendance = new Domain.Entites.HR.Attendance
            {
                EmployeeId = employee.Id,

                ShiftId = employee.ShiftId,

                AttendanceDate = DateOnly.FromDateTime(now),

                CheckInTime = now,

                AttendanceStatus = calculation.Status,

                LateMinutes = calculation.LateMinutes,

                EarlyLeaveMinutes = 0,

                ActualWorkedTime = TimeSpan.Zero,

                Overtime = TimeSpan.Zero,

                Source = AttendanceSource.QR,

                IsLocked = false
            };

            _context.Attendances.Add(newAttendance);

            await _context.SaveChangesAsync(ct);

            var response = new AttendanceResponse
            {
                Id = newAttendance.Id,
                AttendanceDate = newAttendance.AttendanceDate,
                CheckInTime = newAttendance.CheckInTime!.Value,
                LateMinutes = newAttendance.LateMinutes,
                AttendanceStatus = newAttendance.AttendanceStatus,
                Message = "Check-in completed successfully."
            };

            return ResponseFactory.Success(
                response,
                "Attendance recorded successfully.");
        }
    }

}
