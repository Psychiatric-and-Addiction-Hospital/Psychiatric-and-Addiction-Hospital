using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.Features
{
    public class ChatMessage:BaseEntity
    {
        public string Message { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;

        public MessageStatus Status { get; set; } = MessageStatus.Sent;
        public MessageType Type { get; set; } = MessageType.Text;

        public string? MediaUrl { get; set; }
        public bool IsRead { get; set; }

        public string SenderId { get; set; }
        public AppUser Sender { get; set; }

        public string ReceiverId { get; set; }
        public AppUser Receiver { get; set; }

        public string ConversationId
        {
            get
            {
                var ids = new[] { SenderId, ReceiverId };
                Array.Sort(ids);
                return $"{ids[0]}_{ids[1]}";
            }
        }
    }
}
