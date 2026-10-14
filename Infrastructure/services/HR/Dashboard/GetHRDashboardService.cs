using Application.Common.Interfaces.HR.Dashboard;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.Dashboard;

namespace Infrastructure.services.HR.Dashboard
{
    public class GetHRDashboardService : IGetHRDashboard
    {
        private readonly IGetDashboardSummary _summary;

        private readonly IGetEmployeesByDepartment _department;

        private readonly IGetEmployeesByEmploymentStatus _employmentStatus;

        private readonly IGetAttendanceDashboard _attendance;

        private readonly IGetRecruitmentDashboard _recruitment;

        private readonly IGetLeaveDashboard _leave;

        public GetHRDashboardService(IGetDashboardSummary summary,
            IGetEmployeesByDepartment department,
            IGetEmployeesByEmploymentStatus employmentStatus,
            IGetAttendanceDashboard attendance, IGetRecruitmentDashboard recruitment,
            IGetLeaveDashboard leave)
        {
            _summary = summary;
            _department = department;
            _employmentStatus = employmentStatus;
            _attendance = attendance;
            _recruitment = recruitment;
            _leave = leave;
        }
        public async Task<BaseResponse<HRDashboardResponse>> GetAsync(CancellationToken ct)
        {
            var summaryTask = _summary.GetAsync(ct);

            var departmentTask = _department.GetAsync(ct);

            var employmentTask = _employmentStatus.GetAsync(ct);

            var attendanceTask = _attendance.GetAsync(ct);

            var recruitmentTask = _recruitment.GetAsync(ct);

            var leaveTask = _leave.GetAsync(ct);

            await Task.WhenAll(summaryTask, departmentTask, employmentTask, attendanceTask, recruitmentTask, leaveTask);

            if (!summaryTask.Result.Success)
                return ResponseFactory.Fail<HRDashboardResponse>(summaryTask.Result.Message);

            if (!departmentTask.Result.Success)
                return ResponseFactory.Fail<HRDashboardResponse>(departmentTask.Result.Message);

            if (!employmentTask.Result.Success)
                return ResponseFactory.Fail<HRDashboardResponse>(employmentTask.Result.Message);

            if (!attendanceTask.Result.Success)
                return ResponseFactory.Fail<HRDashboardResponse>(attendanceTask.Result.Message);

            if (!recruitmentTask.Result.Success)
                return ResponseFactory.Fail<HRDashboardResponse>(recruitmentTask.Result.Message);

            var response = new HRDashboardResponse
            {
                Summary = summaryTask.Result.Data!,

                EmployeesByDepartment =  departmentTask.Result.Data!,

                EmployeesByEmploymentStatus = employmentTask.Result.Data!,

                Attendance = attendanceTask.Result.Data!,

                Recruitment = recruitmentTask.Result.Data!,

                Leave = leaveTask.Result.Data!
            };

            return ResponseFactory.Success(response, "Dashboard loaded successfully.");
        }

    }
}
