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
    public class TagsConfig : IEntityTypeConfiguration<Tages>
    {
        public void Configure(EntityTypeBuilder<Tages> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}
