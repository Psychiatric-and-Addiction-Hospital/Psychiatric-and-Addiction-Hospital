using Application.Commands.Department;
using Application.Queries.Depertment;
using Application.Queries.Depertments;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers
{
   
    public class DepartmentController : BaseController
    {
        private readonly ISender _sender;
        public DepartmentController(ISender sender)
        {
            _sender = sender;
        }


        [HttpPost("CreateDepartment")]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentCommand request)
        {
            var result = await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment([FromBody] UpdateDepartmentCommand request)
        {
            var result = await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeleteDepartment/{DepartmentId}")]
        public async Task<IActionResult> DeleteDepartment(Guid DepartmentId)
        {
            var result = await _sender.Send(new DeleteDepartmentCommand(DepartmentId));
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpGet("GetAllDepertment")]
        public async Task<IActionResult> GetAllDepertment()
        {
            var result = await _sender.Send(new GetDepertmentQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("DepertmentById/{Id}")]
        public async Task<IActionResult> GetDepertmentById(Guid Id)
        {
            var result = await _sender.Send(new GetDepertmentByIdQuery(Id));
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
