using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations
{
    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> builder)
        {
            builder.HasOne(R => R.UploadedBy)
                .WithMany(A => A.Resource)
                .HasForeignKey(R => R.UploadedById)
                .OnDelete(DeleteBehavior.SetNull);

                 builder.Property(r => r.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(r => r.FileUrl)
                   .IsRequired();

            builder.Property(r => r.UploadedAt)
                   .HasDefaultValueSql("GETDATE()");
        }
    }
}