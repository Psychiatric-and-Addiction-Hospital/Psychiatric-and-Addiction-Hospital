using Application.Commands.HR.Employee;
using Application.DTOS.Request.HR.Employee;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    [Authorize(Policy = "HRManagement")]
    public class HireEmployeeController : BaseController
    {
        private readonly ISender _sender;
        public HireEmployeeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] HireEmployeeRequest request)
        {
            var result = await _sender.Send(new HireEmployeeCommand(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
