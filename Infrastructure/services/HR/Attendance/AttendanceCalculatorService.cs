using Application.Common.Interfaces.HR.Attendance;
using Application.DTOS.Responses.HR.Attendance;
using Domain.Enums.HR;
using employeeEntity = Domain.Entites.HR.Employee;

namespace Infrastructure.services.HR.Attendance
{
    public class AttendanceCalculatorService : IAttendanceCalculator
    {
        public AttendanceCalculationResultResponse Calculate(
            employeeEntity employee,
            DateTime checkIn,
            DateTime? checkOut)
        {
            ArgumentNullException.ThrowIfNull(employee);

            ArgumentNullException.ThrowIfNull(employee.Shift);

            var result = new AttendanceCalculationResultResponse();

            var shift = employee.Shift;

            //---------------------------------------------------
            // Calculate Shift Start & End
            //---------------------------------------------------

            var workDate = checkIn.Date;

            if (shift.IsNightShift &&
                checkIn.TimeOfDay < shift.EndTime.ToTimeSpan())
            {
                workDate = workDate.AddDays(-1);
            }

            var shiftStart = workDate.Add(shift.StartTime.ToTimeSpan());

            var shiftEnd = shift.IsNightShift
                ? workDate.AddDays(1).Add(shift.EndTime.ToTimeSpan())
                : workDate.Add(shift.EndTime.ToTimeSpan());

            result.ShiftStart = shiftStart;
            result.ShiftEnd = shiftEnd;

            //---------------------------------------------------
            // Late Minutes
            //---------------------------------------------------

            var allowedArrival =
                shiftStart.AddMinutes(shift.ToleranceMinutes);

            if (checkIn > allowedArrival)
            {
                result.LateMinutes =
                    (int)(checkIn - shiftStart).TotalMinutes;

                result.Status = AttendanceStatus.Late;
            }
            else
            {
                result.LateMinutes = 0;

                result.Status = AttendanceStatus.Present;
            }

            //---------------------------------------------------
            // Checkout Calculations
            //---------------------------------------------------

            if (checkOut.HasValue)
            {
                var worked = checkOut.Value - checkIn;

                if (worked < TimeSpan.Zero)
                    worked = TimeSpan.Zero;

                result.WorkedTime = worked;

                //---------------------------------------
                // Early Leave
                //---------------------------------------

                if (checkOut.Value < shiftEnd)
                {
                    result.EarlyLeaveMinutes =
                        (int)(shiftEnd - checkOut.Value).TotalMinutes;
                }

                //---------------------------------------
                // Overtime
                //---------------------------------------

                if (checkOut.Value > shiftEnd)
                {
                    result.Overtime =
                        checkOut.Value - shiftEnd;
                }
            }
            else
            {
                result.WorkedTime = TimeSpan.Zero;
                result.Overtime = TimeSpan.Zero;
                result.EarlyLeaveMinutes = 0;
            }

            return result;
        }
    }
}