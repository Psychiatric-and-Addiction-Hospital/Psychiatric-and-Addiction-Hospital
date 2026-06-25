using Application.Commands.ChatMessage;
using Application.Common.Interfaces.ChatMessage;
using Application.Common.Responses;
using Application.DTOS.Responses.ChatMessage;
using Domain.Enums;
using Infrastructure.Hubs;
using Infrastructure.Persistence.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.ChatMessage
{
    public class ChatMessageService : IChatMessage
    {
        private readonly AddIdentityDbContext _context;
        private readonly IHubContext<ChatHub> _hub;
        public ChatMessageService(AddIdentityDbContext context, IHubContext<ChatHub> hub)
        {
            _context = context;
            _hub = hub;
        }
        public async Task<BaseResponse<SendMessageResponse>> SendToUserAsync(SendMessageCommand commande, CancellationToken ct)
        {
            var message = new Domain.Entites.Features.ChatMessage
            {
                SenderId = commande.SenderId,
                ReceiverId = commande.ReceiverId,
                Message = commande.Message,
                Type = commande.Type,
                MediaUrl = commande.MediaUrl,
                Status = MessageStatus.Sent

            };
            await _context.ChatMessages.AddAsync(message, ct);
            await _context.SaveChangesAsync(ct);

            //push to real-time
            await _hub.Clients.User(commande.ReceiverId).SendAsync("ReceiveMessage", new SendMessageResponse
            {
                MessageId = message.Id,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Message = message.Message,
                Type = message.Type,
                MediaUrl = message.MediaUrl,
                SentAt = message.SentAt
            }, ct);

            return ResponseFactory.Success(new SendMessageResponse
            {
                MessageId = message.Id,
                SenderId = message.SenderId,
                ReceiverId = message.ReceiverId,
                Message = message.Message,
                Type = message.Type,
                MediaUrl = message.MediaUrl,
                SentAt = message.SentAt
            }, "Message sent successfully");
        }
    }

}
