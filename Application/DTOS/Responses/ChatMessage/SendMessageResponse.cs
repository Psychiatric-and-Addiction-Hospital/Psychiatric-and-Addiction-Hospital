using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.Responses.ChatMessage
{
    public class SendMessageResponse
    {
        public Guid MessageId { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Message { get; set; }
        public string? MediaUrl { get; set; }
        public MessageType Type { get; set; }
        public DateTime SentAt { get; set; }
    }
}
