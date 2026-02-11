using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.Authentication
{
    public class RefreshToken:BaseEntity
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }
        public bool IsUsed { get; set; }

        public string UserId { get; set; }
        public AppUser User { get; set; }
    }
}
