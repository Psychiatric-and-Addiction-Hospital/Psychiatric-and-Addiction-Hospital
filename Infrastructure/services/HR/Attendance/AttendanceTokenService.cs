using Application.Common.Interfaces.HR.Attendance;
using Application.DTOS.Responses.HR;
using Application.DTOS.Responses.HR.Attendance;
using Microsoft.AspNetCore.DataProtection;
using System.Text.Json;

namespace Infrastructure.services.HR.Attendance
{
    public class AttendanceTokenService : IAttendanceToken
    {
        private readonly IDataProtector _protector;

        public AttendanceTokenService(IDataProtectionProvider provider)
        {
            _protector = provider.CreateProtector("Attendance.QR.Token");
        }

        public GenerateAttendanceQrResponse GenerateToken(int expireAfterSeconds = 60)
        {

            var payload = new AttendanceQrPayloadResponse
            {
                Nonce = Guid.NewGuid(),
                IssuedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddSeconds(expireAfterSeconds),
                Version = 1
            };

            var json = JsonSerializer.Serialize(payload);

            return new GenerateAttendanceQrResponse
            {
                Token = _protector.Protect(json),
                ExpiresAt = payload.ExpiresAt
            };
        }

        public bool TryValidateToken(
            string token,
            out AttendanceQrPayloadResponse payload)
        {
            payload = null!;

            try
            {
                var json = _protector.Unprotect(token);

                payload = JsonSerializer.Deserialize<AttendanceQrPayloadResponse>(json)!;

                if (payload == null)
                    return false;

                if (payload.ExpiresAt <= DateTime.UtcNow)
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}