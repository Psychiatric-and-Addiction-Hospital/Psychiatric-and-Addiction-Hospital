using Application.Commands.HR.Contract;
using Application.Common.Responses;
using Application.DTOS.Request.HR.Contract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    public class ContractController : BaseController
    {
        private readonly ISender _sender;
        public ContractController(ISender sender)
        {
            _sender = sender;
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPost("CreateContract")]
        public async Task<IActionResult> Create([FromBody] CreateContractRequest request)
        {
            var result = await _sender.Send(new CreateContractCommand(request));
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPut("{id:guid}/UpdateContract")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateContractRequest request)
        {
            if (id != request.Id)
                return BadRequest(ResponseFactory.Fail<bool>("Route id does not match request id."));

            var result = await _sender.Send(new UpdateContractCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPut("{id:guid}/SignContract")]
        public async Task<IActionResult> Sign(Guid id)
        {
            var result = await _sender.Send(new SignContractCommand(id));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [Authorize(Policy = "HRManagement")]
        [HttpPut("{id:guid}/SubmitContractForSignature")]
        public async Task<IActionResult> SubmitContractForSignature(Guid id)
        {
            var result = await _sender.Send(new SubmitContractForSignatureCommand(id));

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
