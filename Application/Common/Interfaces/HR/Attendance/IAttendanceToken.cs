using Application.DTOS.Responses.HR;
using Application.DTOS.Responses.HR.Attendance;
using System;

namespace Application.Common.Interfaces.HR.Attendance
{
    public interface IAttendanceToken
    {
        GenerateAttendanceQrResponse GenerateToken(int expireAfterSeconds = 60);

        bool TryValidateToken(
            string token,
            out AttendanceQrPayloadResponse payload);
    }
}
