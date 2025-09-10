using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.BlogModule
{
    public class BlogPost
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime PublishedAt { get; set; } = DateTime.UtcNow;
        public bool IsPublished{get; set; }

        public string AuthorId { get; set; }
        public AppUser Author { get; set; }

        public Guid CategoryId { get; set; }
        public BlogCategory Category { get; set; }

        public ICollection<Comments> Comments { get; set; }=new List<Comments>();
        public ICollection<BlogPostTag> BlogPostTags { get; set; } = new List<BlogPostTag>();


    }
}
