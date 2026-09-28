using Application.Commands.ChatMessage;
using Application.Queries.ChatMessage;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Psychiatric_and_Addiction_Hospital.Controllers
{
    [Authorize]
    public class ChatMessageController : BaseController
    {
        private readonly ISender _sender;

        public ChatMessageController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Send a new message to another user
        /// POST /api/ChatMessage/send
        /// </summary>
        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageCommand command, CancellationToken ct)
        {
            var result = await _sender.Send(command, ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Get conversation (messages) between two users with pagination
        /// GET /api/ChatMessage/conversation?userId1=...&amp;userId2=...&amp;pageNumber=1&amp;pageSize=20
        /// </summary>
        [HttpGet("conversation")]
        public async Task<IActionResult> GetConversation(
            [FromQuery] string userId1,
            [FromQuery] string userId2,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 20,
            CancellationToken ct = default)
        {
            var result = await _sender.Send(
                new GetConversationQuery(userId1, userId2, pageNumber, pageSize), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}