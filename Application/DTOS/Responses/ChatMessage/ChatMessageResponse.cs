using Domain.Enums;
using System;

namespace Application.DTOS.Responses.ChatMessage
{
    public class ChatMessageResponse
    {
        public Guid Id { get; set; }

        public string SenderId { get; set; }
        public string ReceiverId { get; set; }

        public string Message { get; set; }

        public MessageType Type { get; set; }

        public string? MediaUrl { get; set; }

        public DateTime SentAt { get; set; }

        public MessageStatus Status { get; set; }
    }
}
