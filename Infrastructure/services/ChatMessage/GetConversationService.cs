using Application.Common.Interfaces.ChatMessage;
using Application.Common.Responses;
using Application.DTOS.Responses.ChatMessage;
using Application.Queries.ChatMessage;
using Domain.Helpers;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.ChatMessage
{
    public class GetConversationServic: IGetConversation
    {
        private readonly AddIdentityDbContext _context;
        public GetConversationServic(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<List<ChatMessageResponse>>> GetConversationAsync(GetConversationQuery query, CancellationToken ct)
        {
            var conversationId = ChatHelper.GetConversationId(query.UserId1, query.UserId2);

            var chatMessages = await _context.ChatMessages
                .AsNoTracking()
                .Where(x => x.ConversationId == conversationId)
                .OrderBy(x => x.SentAt)
                .Skip((query.PageNumber - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(x => new ChatMessageResponse
                {
                    Id = x.Id,
                    Message = x.Message,
                    SenderId = x.SenderId,
                    ReceiverId = x.ReceiverId,
                    SentAt = x.SentAt,
                    Type = x.Type,
                    MediaUrl = x.MediaUrl,
                    Status = x.Status
                })
                .ToListAsync(ct);
            if (chatMessages == null || chatMessages.Count == 0)
            {
                return ResponseFactory.Fail<List<ChatMessageResponse>>("No messages found in this conversation.");
            }
            return ResponseFactory.Success(chatMessages);
        }
    }
}
