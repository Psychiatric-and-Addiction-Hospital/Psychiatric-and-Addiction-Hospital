using Application.DTOS.Request.Doctor;
using Application.Queries.Doctor.ManagementDoctor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.Doctor
{

    public class ManagementDoctorController : BaseController
    {
        private readonly ISender _Sender;
        public ManagementDoctorController(ISender sender)
        {

            _Sender = sender;
        }

        [HttpGet("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors([FromQuery] DoctorListRequest request)
        {
            var result = await _Sender.Send(new GetAllDoctorsQuery(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetDoctor/{Id}")]
        public async Task<IActionResult> GetDoctorById(Guid Id)
        {
            var result = await _Sender.Send(new GetDoctorByIdQuery(Id));
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}