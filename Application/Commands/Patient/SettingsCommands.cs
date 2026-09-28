using Application.Common.Responses;
using MediatR;

namespace Application.Commands.Patient
{
    // PUT /api/Settings/security  ← تغيير كلمة المرور
    public record ChangePasswordCommand(
        string UserId,
        string CurrentPassword,
        string NewPassword,
        string ConfirmNewPassword
    ) : IRequest<BaseResponse<Unit>>;

    // PUT /api/Settings/preferences  ← تحديث التفضيلات
    public record UpdatePreferencesCommand(
        string UserId,
        bool EmailNotifications,
        bool SmsNotifications,
        bool AppNotifications,
        string Language        // "ar" | "en"
    ) : IRequest<BaseResponse<Unit>>;
}
