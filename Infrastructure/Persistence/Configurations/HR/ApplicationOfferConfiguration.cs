using Domain.Entites.HR.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Configurations.HR
{
    public class ApplicationOfferConfiguration : IEntityTypeConfiguration<ApplicationOffer>
    {
        public void Configure(EntityTypeBuilder<ApplicationOffer> builder)
        {
            builder.ToTable("ApplicationOffers");

           

            builder.Property(o => o.OfferedSalary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(o => o.IsAccepted)
                .HasDefaultValue(false);

            builder.Property(o => o.ExpiryDate)
                .IsRequired();

            builder.Property(o => o.statues)
                          .HasConversion<string>()
                          .HasMaxLength(50)
                          .IsRequired();


            builder.HasOne(o => o.ApplicationProcess)
                .WithMany()
                .HasForeignKey(o => o.ApplicationProcessId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
