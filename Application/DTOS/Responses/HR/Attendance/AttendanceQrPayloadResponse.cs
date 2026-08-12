using System;

namespace Application.DTOS.Responses.HR.Attendance
{
    public class AttendanceQrPayloadResponse
    {
        public Guid Nonce { get; set; }

        public DateTime IssuedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        public int Version { get; set; } = 1;
    }
}
