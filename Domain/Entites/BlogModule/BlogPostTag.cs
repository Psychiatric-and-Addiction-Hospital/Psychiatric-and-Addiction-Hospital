using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.BlogModule
{
    public class BlogPostTag
    {
        public Guid BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }

        public Guid BlogTagId { get; set; }
        public Tages BlogTag { get; set; }
    }
}
