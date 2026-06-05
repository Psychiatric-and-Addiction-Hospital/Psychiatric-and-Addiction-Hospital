using Application.Commands.Session;
using Application.Commands.Sessions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Psychiatric_and_Addiction_Hospital.Controllers.Sessions
{
    public class SessionController : BaseController
    {
        private readonly ISender _sender;
        public SessionController(ISender sender) => _sender = sender;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSessionCommand command)
        {
            var result = await _sender.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateSessionStatusCommand command)
        {
            // للتأكد من أن الـ ID في المسار هو نفسه في الـ Body
            if (id != command.SessionId) return BadRequest();

            var result = await _sender.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPut("{id}/notes")]
        [Authorize(Roles = "Doctor")] // تأكد من أن الطبيب فقط هو من يمكنه إضافة الملاحظات
        public async Task<IActionResult> UpdateNotes(Guid id, [FromBody] string notes)
        {
            var result = await _sender.Send(new UpdateSessionNotesCommand(id, notes));

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody] EditSessionCommand command)
        {
            var result = await _sender.Send(command);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}