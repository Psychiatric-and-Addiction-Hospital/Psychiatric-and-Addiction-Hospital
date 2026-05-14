using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses.Session
{
    public record SessionShortResponse
    {
            public Guid Id { get; init; }
            public DateTime ScheduledDate { get; init; }
            public int DurationMinutes { get; init; }
            public string Status { get; init; } // نص الحالة (Scheduled, Completed, etc.)
            public string SessionType { get; init; }

            // بيانات المريض الأساسية فقط
            public string PatientId { get; init; }
            public string PatientName { get; init; }

            // بيانات الطبيب (إذا كان الطلب من جهة المريض أو الإدارة)
            public string DoctorName { get; init; }
    }
}
