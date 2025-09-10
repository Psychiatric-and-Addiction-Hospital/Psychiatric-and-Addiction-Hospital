using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites.BlogModule
{
    public class Tages
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }=string.Empty;
        public ICollection<BlogPostTag> Plogposttags { get; set; }=new List<BlogPostTag>();
    }
}
