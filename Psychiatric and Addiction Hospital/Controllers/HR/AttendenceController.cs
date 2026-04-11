using Application.Commands.HR.Attendance;
using Application.Queries.HR.Attendance;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceController : BaseController
    {
        private readonly ISender _sender;

        public AttendenceController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("CheckIn/{employeeId}")]
        public async Task<IActionResult> CheckIn(Guid employeeId)
        {
            var result = await _sender.Send(new CreateAttendanceCommand(employeeId));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("ByEmployee/{employeeId}")]
        public async Task<IActionResult> GetByEmployee(Guid employeeId)
        {
            var result = await _sender.Send(new GetAttendanceByEmployeeQuery(employeeId));
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
