namespace Application.DTOS.Request.HR.Attendance
{
    public class GenerateAttendanceQrRequest
    {
        public int ExpireAfterSeconds { get; set; } = 60;
    }
}
