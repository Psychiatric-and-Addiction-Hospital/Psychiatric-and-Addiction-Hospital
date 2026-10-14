using Application.Common.Responses;
using MediatR;

namespace Application.Commands.Notification
{
    public record MarkAllNotificationsAsReadCommand(string UserId)
        : IRequest<BaseResponse<Unit>>;
}
