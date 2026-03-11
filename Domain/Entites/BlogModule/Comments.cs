using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.BlogModule
{
    public class Comments: BaseEntity
    {
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;
        public string AuthorId { get; set; }
        public AppUser Author { get; set; }

        public Guid BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }
    }
}
