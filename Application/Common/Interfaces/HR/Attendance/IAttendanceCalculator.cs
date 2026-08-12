using Application.DTOS.Responses.HR.Attendance;
using System;


namespace Application.Common.Interfaces.HR.Attendance
{
    public interface IAttendanceCalculator
    {
        AttendanceCalculationResultResponse Calculate(
         Domain.Entites.HR.Employee employee,
          DateTime checkIn,
          DateTime? checkOut);
    }
}
