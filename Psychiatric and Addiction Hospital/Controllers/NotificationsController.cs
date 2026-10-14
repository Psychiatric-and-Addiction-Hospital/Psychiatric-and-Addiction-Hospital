using Application.Commands.Notification;
using Application.Queries.Notification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Psychiatric_and_Addiction_Hospital.Controllers
{
    [Authorize]
    public class NotificationsController : BaseController
    {
        private readonly ISender _sender;

        public NotificationsController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Get all notifications for the currently logged-in user
        /// GET /api/Notifications
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetMyNotifications(CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _sender.Send(new GetUserNotificationsQuery(userId), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Mark a single notification as read
        /// POST /api/Notifications/{id}/read
        /// </summary>
        [HttpPost("{id:guid}/read")]
        public async Task<IActionResult> MarkAsRead(Guid id, CancellationToken ct)
        {
            var result = await _sender.Send(new MarkNotificationAsReadCommand(id), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// Mark all notifications as read for the currently logged-in user
        /// POST /api/Notifications/read-all
        /// </summary>
        [HttpPost("read-all")]
        public async Task<IActionResult> MarkAllAsRead(CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var result = await _sender.Send(new MarkAllNotificationsAsReadCommand(userId), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
