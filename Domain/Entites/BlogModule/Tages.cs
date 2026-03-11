using Domain.Common;
using System.Collections.Generic;


namespace Domain.Entites.BlogModule
{
    public class Tages: BaseEntity
    {
        public string Name { get; set; }=string.Empty;
        public ICollection<BlogPostTag> BlogPostTags { get; set; }=new List<BlogPostTag>();
    }
}
