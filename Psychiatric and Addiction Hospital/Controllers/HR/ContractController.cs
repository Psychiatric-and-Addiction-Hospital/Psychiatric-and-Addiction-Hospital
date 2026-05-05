using Application.Commands.HR.Contract;
using Application.Queries.HR.Contract;
using Domain.Entites.HR;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{

    public class ContractController : ControllerBase
    {
        private readonly ISender _Sender;

        public ContractController(ISender sender)
        {
            _Sender = sender;
        }

        [HttpPost("CreateContract")]
        public async Task<IActionResult> CreateContract([FromBody] CreateContractCommand request)
        {
            var result = await _Sender.Send(request);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UpdateContract/{Id}")]
        public async Task<IActionResult> UpdateContract(Guid Id, Guid EmployeeId, DateTime StartDate, DateTime? EndDate, string Terms, decimal BaseSalary)
        {
            var result = await _Sender.Send(
                new UpdateContractCommand( Id,  EmployeeId,  StartDate,EndDate,  Terms,  BaseSalary));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("DeleteContract/{id}")]
        public async Task<IActionResult> DeleteContract(Guid id)
        {
            var result = await _Sender.Send(new DeleteContractCommand(id));
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpGet("GetContractById/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _Sender.Send(new GetContractByIdQuery(id));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetAllContracts")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _Sender.Send(new GetAllContractsQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
