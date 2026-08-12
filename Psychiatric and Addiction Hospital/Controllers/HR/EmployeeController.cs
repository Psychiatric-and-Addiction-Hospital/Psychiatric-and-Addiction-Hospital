using Application.Commands.HR.Employee;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Employee;
using Application.Queries.HR.Employee;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{

    public class EmployeeController : BaseController
    {
        private readonly ISender _sender;
        public EmployeeController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id:guid}/DeleteEmployee")]
        public async Task<IActionResult> Delete(Guid id, [FromBody] DeleteEmployeeRequest request)
        {
            if (id != request.EmployeeId)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));

            var result = await _sender.Send(new DeleteEmployeeCommand(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPut("{id:guid}/UpdateEmployee")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeRequest request)
        {
            if (id != request.EmployeeId)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));

            var result = await _sender.Send(new UpdateEmployeeCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAll([FromQuery] EmployeeListRequest request)
        {
            var result = await _sender.Send(new GetEmployeesQuery(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpGet("{id:guid}/GetByIdEmployee")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _sender.Send(new GetEmployeeByIdQuery(id));

            return result.Success ? Ok(result) : NotFound(result);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("{id:guid}/RestoreEmployee")]
        public async Task<IActionResult> RestoreEmployee(Guid id, [FromBody] RestoreEmployeeRequest request)
        {
            if (id != request.EmployeeId)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));

            var result = await _sender.Send(new RestoreEmployeeCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
