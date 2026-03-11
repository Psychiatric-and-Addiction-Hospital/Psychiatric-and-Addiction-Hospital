using Domain.Common;
using System.Collections.Generic;


namespace Domain.Entites.BlogModule
{
    public class BlogCategory: BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    }
}
