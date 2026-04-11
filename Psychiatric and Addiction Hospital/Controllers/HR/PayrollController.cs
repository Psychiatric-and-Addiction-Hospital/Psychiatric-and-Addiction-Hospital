using Application.Commands.HR.Payroll;
using Application.Queries.HR.Payroll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : BaseController
    {
        private readonly ISender _sender;

        public PayrollController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("CreatePayroll")]
        public async Task<IActionResult> CreatePayroll([FromBody] CreatePayrollCommand request)
        {
            var result = await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UpdatePayroll")]
        public async Task<IActionResult> UpdatePayroll([FromBody] UpdatePayrollCommand request)
        {
            var result = await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeletePayroll/{id}")]
        public async Task<IActionResult> DeletePayroll(Guid id)
        {
            var result = await _sender.Send(new DeletePayrollCommand(id));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetPayrollById/{id}")]
        public async Task<IActionResult> GetPayrollById(Guid id)
        {
            var result = await _sender.Send(new GetPayrollByIdQuery(id));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetAllPayrolls")]
        public async Task<IActionResult> GetAllPayrolls()
        {
            var result = await _sender.Send(new GetAllPayrollsQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
