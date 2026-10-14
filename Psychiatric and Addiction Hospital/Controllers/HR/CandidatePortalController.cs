
using Application.Commands.HR.ApplicationOffer;
using Application.Commands.HR.CandidatePortal;
using Application.Common.Constants;
using Application.Common.Interfaces.HR.CandidatePortal;
using Application.DTOS.Request.HR.Candidate;
using Application.Queries.HR.CandidatePortal;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.HR
{
    [Authorize(Policy = "CandidateOnly")]
    public class CandidatePortalController : BaseController
    {
        private readonly ISender _sender;
        public CandidatePortalController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("account")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateAccount([FromBody] CreateCandidateAccountRequest request)
        {
            var result = await _sender.Send(new CreateCandidateAccountCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpGet("GetMyProfile")]
        public async Task<IActionResult> GetMyProfile()
        {
            var result = await _sender.Send(new GetMyCandidateProfileQuery());

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateMyProfile([FromForm] UpdateCandidateProfileRequest request)
        {
            var result = await _sender.Send(new UpdateMyCandidateProfileCommand(request));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetMyOffers")]
        public async Task<IActionResult> GetMyOffers()
        {
            var result = await _sender.Send(new GetMyOffersQuery());
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("candidate/contracts/{contractId:guid}/sign")]
        public async Task<IActionResult> SignContract(Guid contractId)
        {
            var result = await _sender.Send(new SignContractCommand(contractId));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("GetMyApplications")]
        public async Task<IActionResult> GetMyApplications()
        {
            var result = await _sender.Send(new GetMyApplicationsQuery());

            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpPut("{id:guid}/AcceptApplicationOffer")]
        public async Task<IActionResult> Accept(Guid id)
        {
            var result = await _sender.Send(new AcceptApplicationOfferCommand(id));

            return result.Success ? Ok(result) : BadRequest(result);
        }


        [HttpPut("{id:guid}/RejectApplicationOffer")]
        public async Task<IActionResult> Reject(Guid id)
        {
            var result = await _sender.Send(new RejectApplicationOfferCommand(id));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id:guid}/WithdrawApplication")]
        public async Task<IActionResult> Withdraw(Guid id)
        {
            var result = await _sender.Send(new WithdrawApplicationCommand(id));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("my-applications/{applicationId:guid}/status-history")]
        public async Task<IActionResult> GetStatusHistory([FromRoute] Guid applicationId)
        {
            var result = await _sender.Send(
                new GetApplicationStatusHistoryQuery(applicationId));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _sender.Send(new GetCandidateDashboardQuery());

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("CandidateInterview")]
        public async Task<IActionResult> GetUpcoming()
        {
            var result = await _sender.Send(new CandidateInterviewQuery());

            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
