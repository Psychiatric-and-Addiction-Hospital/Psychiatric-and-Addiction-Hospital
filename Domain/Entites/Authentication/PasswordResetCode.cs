using Domain.Common;
using System;


namespace Domain.Entites.Authentication
{
    public class PasswordResetCode:BaseEntity
    {
        
        public string Code { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
