using Application.Commands.HR.Attendance;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Attendance;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.Attendance
{
    public class ManualAttendanceService : IManualAttendance
    {

        private readonly AddIdentityDbContext _context;
        private readonly IAttendanceCalculator _calculator;
        private readonly ICurrentUser _currentUser;

        public ManualAttendanceService(
            AddIdentityDbContext context, IAttendanceCalculator calculator, ICurrentUser currentUser)
        {
            _context = context;
            _calculator = calculator;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<AttendanceResponse>> SaveAsync(
            ManualAttendanceCommand request,
            CancellationToken ct)
        {
            var employee = await _context.Employees
                .Include(x => x.Shift)
                .FirstOrDefaultAsync(
                    x => x.Id == request.Request.EmployeeId, ct);

            if (employee == null)
                return ResponseFactory.Fail<AttendanceResponse>(
                    "Employee not found.");

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(
                    x => x.EmployeeId == request.Request.EmployeeId &&
                         x.AttendanceDate == request.Request.AttendanceDate,
                    ct);

            bool isNew = attendance == null;

            if (isNew)
            {
                attendance = new Domain.Entites.HR.Attendance
                {
                    EmployeeId = employee.Id,
                    ShiftId = employee.ShiftId,
                    AttendanceDate = request.Request.AttendanceDate
                };

                _context.Attendances.Add(attendance);
            }

            //---------------------------------------------------
            // Manual Values
            //---------------------------------------------------

            attendance.CheckInTime = request.Request.CheckInTime;
            attendance.CheckOutTime = request.Request.CheckOutTime;
            attendance.Remarks = request.Request.Remarks;

            attendance.Source = AttendanceSource.Manual;

            attendance.IsLocked = request.Request.LockAfterSave;

            attendance.ModifiedAt = DateTime.UtcNow;

            attendance.ModifiedBy = _currentUser.UserId;

            attendance.ModificationReason =
                request.Request.ModificationReason;

            //---------------------------------------------------
            // Auto Calculate
            //---------------------------------------------------

            if (request.Request.CheckInTime.HasValue)
            {
                var result = _calculator.Calculate(
                    employee,
                    request.Request.CheckInTime.Value,
                    request.Request.CheckOutTime);

                attendance.LateMinutes = result.LateMinutes;

                attendance.EarlyLeaveMinutes =
                    result.EarlyLeaveMinutes;

                attendance.ActualWorkedTime =
                    result.WorkedTime;

                attendance.Overtime =
                    result.Overtime;

                attendance.AttendanceStatus =
                    request.Request.AttendanceStatus
                    ?? result.Status;
            }
            else
            {
                attendance.AttendanceStatus =
                    request.Request.AttendanceStatus
                    ?? AttendanceStatus.Absent;
            }

            //---------------------------------------------------
            // Save
            //---------------------------------------------------

            await _context.SaveChangesAsync(ct);

            //---------------------------------------------------
            // Response
            //---------------------------------------------------

            return ResponseFactory.Success(
                new AttendanceResponse
                {
                    Id = attendance.Id,

                    AttendanceDate = attendance.AttendanceDate,

                    CheckInTime = attendance.CheckInTime,

                    CheckOutTime = attendance.CheckOutTime,

                    AttendanceStatus = attendance.AttendanceStatus,

                    LateMinutes = attendance.LateMinutes,

                    EarlyLeaveMinutes = attendance.EarlyLeaveMinutes,

                    WorkedTime = attendance.ActualWorkedTime,

                    Overtime = attendance.Overtime,

                    Source = attendance.Source,

                    IsLocked = attendance.IsLocked,

                    Remarks = attendance.Remarks,

                    Message = isNew
                        ? "Manual attendance created successfully."
                        : "Manual attendance updated successfully."
                },
                "Success");
        }
    }
}

