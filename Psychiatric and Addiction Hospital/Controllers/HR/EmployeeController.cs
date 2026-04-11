using Application.Commands.HR.Employee;
using Application.Queries.Employee;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        private readonly ISender _sender;

        public EmployeeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeCommand request)
        {
            var result = await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee([FromBody] UpdateEmployeeCommand request)
        {
            var result = await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var result = await _sender.Send(new DeleteEmployeeCommand(id));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await _sender.Send(new GetAllEmployeesQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var result = await _sender.Send(new GetEmployeeByIdQuery(id));
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
