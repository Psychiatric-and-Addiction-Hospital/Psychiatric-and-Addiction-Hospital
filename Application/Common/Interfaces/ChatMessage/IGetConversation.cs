using Application.Common.Responses;
using Application.DTOS.Responses.ChatMessage;
using Application.Queries.ChatMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.ChatMessage
{
    public interface IGetConversation
    {
        Task<BaseResponse<List<ChatMessageResponse>>> GetConversationAsync(GetConversationQuery query, CancellationToken ct);
    }
}
