using Application.Queries.HR.Dashboard;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    [Authorize(Policy = "HRManagement")]
    public class DashboardHRController : BaseController
    {
        private readonly ISender _sender;
        public DashboardHRController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("GetHRDashboard")]
        public async Task<IActionResult> GetHRDashboard()
        {
            var result = await _sender.Send(new GetHRDashboardQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("Summary")]
        public async Task<IActionResult> GetSummary()
        {
            var result = await _sender.Send(new GetDashboardSummaryQuery());

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("EmployeesByDepartment")]
        public async Task<IActionResult> GetEmployeesByDepartment()
        {
            var result = await _sender.Send(new GetEmployeesByDepartmentQuery());

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("EmployeesByEmploymentStatus")]
        public async Task<IActionResult> GetEmployeesByEmploymentStatus()
        {
            var result = await _sender.Send(new GetEmployeesByEmploymentStatusQuery());

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetAttendance")]
        public async Task<IActionResult> GetAttendance()
        {
            var result = await _sender.Send(new GetAttendanceDashboardQuery());

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetRecruitment")]
        public async Task<IActionResult> GetRecruitment()
        {
            var result = await _sender.Send(new GetRecruitmentDashboardQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetLeave")]
        public async Task<IActionResult> GetLeave()
        {
            var result = await _sender.Send(new GetLeaveDashboardQuery());

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
