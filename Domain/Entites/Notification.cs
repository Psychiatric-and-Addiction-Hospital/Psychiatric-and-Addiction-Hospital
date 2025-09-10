using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Notification : BaseEntity
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime SentAt { get; set; } = DateTime.Now;
        public bool IsRead { get; set; }
        public NotificationType NotificationType { get; set; }
        public Guid? RelatedSessionId { get; set; }
        public Session? RelatedSession { get; set; }
        public string RecipientId { get;set; }
        public AppUser Recipient { get; set; }

    }
}
