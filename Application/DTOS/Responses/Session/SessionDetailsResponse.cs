using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Responses.Sessions
{
    public record SessionDetailsResponse
    {
        public Guid Id { get; init; }
        public DateTime ScheduledDate { get; init; }
        public DateTime CreatedAt { get; init; }
        public int DurationMinutes { get; init; }
        public SessionStatus Status { get; init; }
        public string? CancellationReason { get; init; }

        // بيانات الطبيب
        public string DoctorId { get; init; }
        public string DoctorName { get; init; }
        public string DoctorSpecialty { get; init; }

        // بيانات المريض
        public string PatientId { get; init; }
        public string PatientName { get; init; }
        public string PatientPhone { get; init; }

        // إحصائيات سريعة مرتبطة بالجلسة
        public int ReportsCount { get; init; }
        public bool HasProgressTracker { get; init; }
    }
}
