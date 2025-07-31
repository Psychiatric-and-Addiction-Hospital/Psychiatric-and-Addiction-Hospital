using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class SupporterConfiguration : IEntityTypeConfiguration<Supporter>
    {
        public void Configure(EntityTypeBuilder<Supporter> builder)
        {
            builder.HasOne(S => S.patient)
                .WithMany(A => A.Supporter)
                .HasForeignKey(S => S.patientId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.Relationship)
                   .HasMaxLength(500);

        }
    }
}
