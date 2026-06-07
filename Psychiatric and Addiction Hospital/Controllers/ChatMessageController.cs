using Application.Commands.ChatMessage;
using Application.Queries.ChatMessage;
using Application.Queries.Services;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers
{
    public class ChatMessageController : BaseController
    {
        private readonly ISender _sender;
        public ChatMessageController(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand command)
        {
            var result = await _sender.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);

        }

        [HttpGet("GetConversation")]
        public async Task<IActionResult> GetConversation()
        {
            var result = await _sender.Send(new GetAllServicesQuery());
            return result.Success ? Ok(result) : BadRequest(result);

        }
    }
}