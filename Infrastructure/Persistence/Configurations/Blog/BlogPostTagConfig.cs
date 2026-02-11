using Domain.Entites.BlogModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations.Blog
{
    public class BlogPostTagConfig : IEntityTypeConfiguration<BlogPostTag>
    {
        public void Configure(EntityTypeBuilder<BlogPostTag> builder)
        {
            builder.HasKey(bt => new { bt.BlogPostId, bt.BlogTagId });

            builder.HasOne(bt => bt.BlogPost)
                   .WithMany(b => b.BlogPostTags)
                   .HasForeignKey(bt => bt.BlogPostId);

            builder.HasOne(bt => bt.BlogTag)
                   .WithMany(t => t.Plogposttags)
                   .HasForeignKey(bt => bt.BlogTagId);
        }
    }
}
