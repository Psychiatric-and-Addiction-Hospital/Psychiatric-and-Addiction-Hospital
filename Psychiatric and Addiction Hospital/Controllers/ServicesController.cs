using Application.Commands.Service;
using Application.Queries.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers
{

    public class ServicesController : BaseController
    {
        private readonly ISender _sender;
        public ServicesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("CreateService")]
        public async Task<IActionResult> CreateService([FromBody] CreateServiceCommand request)
        {
            var result = await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpPut("UpdateService")]
        public async Task<IActionResult> UpdateService([FromBody] UpdateServiceCommand request)
        {
            var result = await _sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeleteService/{ServiceId}")]
        public async Task<IActionResult> DeleteService(Guid ServiceId)
        {
            var result = await _sender.Send(new DeleteServiceCommand(ServiceId));
            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpGet("GetAllServices")]
        public async Task<IActionResult> GetAllServices()
        {
            var result = await _sender.Send(new GetAllServicesQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetService/{ServiceId}")]
        public async Task<IActionResult> GetServiceById(Guid ServiceId)
        {
            var result = await _sender.Send(new GetServiceByIdQuery(ServiceId));
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
