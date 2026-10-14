using Application.Commands.HR.Attendance;
using Application.Common.Interfaces.HR.Attendance;
using Application.DTOS.Request.HR.Attendance;
using Application.Queries.HR.Attendance;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
 
    public class AttendenceController : BaseController
    {
        private readonly ISender _sender;
        private readonly IAttendanceToken _attendanceToken;

        public AttendenceController(ISender sender, IAttendanceToken attendanceToken)
        {
            _sender = sender;
            _attendanceToken = attendanceToken;
        }

        [Authorize(Policy = "AttendanceManagement")]
        [HttpGet("GenerateQr")]
        public IActionResult GenerateQr()
        {
            var result = _attendanceToken.GenerateToken();

            return Ok(result);
        }

        [Authorize(Policy = "HospitalStaff")]
        [HttpPost("check-in")]
        public async Task<IActionResult> CheckIn([FromBody] CheckInRequest request)
        {
            var result = await _sender.Send(new CheckInCommand(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HospitalStaff")]
        [HttpPost("check-out")]
        public async Task<IActionResult> CheckOut([FromBody] CheckOutRequest request)
        {
            var result = await _sender.Send(new CheckOutCommand(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HospitalStaff")]
        [HttpGet("today")]
        public async Task<IActionResult> GetTodayAttendance(CancellationToken ct)
        {
            var result = await _sender.Send(new GetTodayAttendanceQuery(), ct);

           return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
