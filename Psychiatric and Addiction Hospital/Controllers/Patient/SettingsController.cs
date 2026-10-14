using Application.Commands.Patient;
using Application.Queries.Patient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Psychiatric_and_Addiction_Hospital.Controllers.Patient
{
    [Authorize]
    [Route("api/Patient/Settings")]
    public class SettingsController : BaseController
    {
        private readonly ISender _sender;
        public SettingsController(ISender sender) => _sender = sender;

        // ── SECURITY ─────────────────────────────────────────────────────────────

        /// <summary>
        /// PUT /api/Patient/Settings/Security/ChangePassword  ← تغيير كلمة المرور
        /// </summary>
        [HttpPut("Security/ChangePassword")]
        public async Task<IActionResult> ChangePassword(
            [FromBody] ChangePasswordCommand command,
            CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            // Override UserId من الـ JWT بدل ما يجي من الـ body
            var safeCommand = command with { UserId = userId };
            var result = await _sender.Send(safeCommand, ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // ── PREFERENCES ───────────────────────────────────────────────────────────

        /// <summary>
        /// GET /api/Patient/Settings/Preferences/Get
        /// </summary>
        [HttpGet("Preferences/Get")]
        public async Task<IActionResult> GetPreferences(CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _sender.Send(new GetPreferencesQuery(userId), ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        /// <summary>
        /// PUT /api/Patient/Settings/Preferences/Update
        /// </summary>
        [HttpPut("Preferences/Update")]
        public async Task<IActionResult> UpdatePreferences(
            [FromBody] UpdatePreferencesCommand command,
            CancellationToken ct)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var safeCommand = command with { UserId = userId };
            var result = await _sender.Send(safeCommand, ct);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}
