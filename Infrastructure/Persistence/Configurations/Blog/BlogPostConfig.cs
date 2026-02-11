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
    public class BlogPostConfig : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(b => b.Content)
                   .IsRequired();

            builder.Property(b => b.PublishedAt)
                   .HasDefaultValueSql("GETUTCDATE()"); 

            builder.HasOne(b => b.Author)
                   .WithMany(u => u.BlogPosts)
                   .HasForeignKey(b => b.AuthorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Category)
                   .WithMany(c => c.BlogPosts)
                   .HasForeignKey(b => b.CategoryId);

            builder.HasMany(b => b.Comments)
                   .WithOne(c => c.BlogPost)
                   .HasForeignKey(c => c.BlogPostId);
        }
    }
}
