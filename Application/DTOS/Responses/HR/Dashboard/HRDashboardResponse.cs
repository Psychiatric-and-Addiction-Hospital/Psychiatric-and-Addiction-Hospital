using System.Collections.Generic;

namespace Application.DTOS.Responses.HR.Dashboard
{
    public class HRDashboardResponse
    {
        public DashboardSummaryResponse Summary { get; set; } = null!;

        public List<EmployeesByDepartmentResponse> EmployeesByDepartment { get; set; } = [];

        public List<EmployeesByEmploymentStatusResponse> EmployeesByEmploymentStatus { get; set; } = [];

        public AttendanceDashboardResponse Attendance { get; set; } = null!;

        public RecruitmentDashboardResponse Recruitment { get; set; } = null!;

        public LeaveDashboardResponse Leave { get; set; } = null!;
    }
}
