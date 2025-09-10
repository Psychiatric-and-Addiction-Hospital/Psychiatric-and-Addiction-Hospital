using Domain.Entites.BlogModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Blog
{
    public class CommentConfig : IEntityTypeConfiguration<Comments>
    {
        public void Configure(EntityTypeBuilder<Comments> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Content)
                   .IsRequired()
                   .HasMaxLength(1000);

            builder.Property(c => c.CreatedAt)
                   .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(c => c.Author)
                   .WithMany(u => u.Comments)
                   .HasForeignKey(c => c.AuthorId);

            builder.HasOne(c => c.BlogPost)
                   .WithMany(b => b.Comments)
                   .HasForeignKey(c => c.BlogPostId);
        }
    }
}
