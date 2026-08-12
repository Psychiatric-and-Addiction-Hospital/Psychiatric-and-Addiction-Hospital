using System;

namespace Application.DTOS.Responses.HR
{
    public class GenerateAttendanceQrResponse
    {
        public string Token { get; set; } = string.Empty;

        public DateTime ExpiresAt { get; set; }
    }
}
