using Application.Commands.ChatMessage;
using Application.Common.Responses;
using Application.DTOS.Responses.ChatMessage;

using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.ChatMessage
{
    public interface IChatMessage
    {
        Task<BaseResponse<SendMessageResponse>> SendToUserAsync(SendMessageCommand commande, CancellationToken ct);
    }
}
