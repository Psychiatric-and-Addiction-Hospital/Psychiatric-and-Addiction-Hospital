using System.Collections.Generic;

namespace Application.DTOS.Responses
{
    public class PatientDashboardResponse
    {
        public SessionSummaryResponse? NextAppointment { get; set; }
        public List<SessionNoteResponse> RecentNotes { get; set; } = new();
        public List<SessionSummaryResponse> RecentSessions { get; set; } = new();
    }
}
