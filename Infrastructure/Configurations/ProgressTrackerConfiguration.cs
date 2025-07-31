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
    public class ProgressTrackerConfiguration : IEntityTypeConfiguration<ProgressTracker>
    {
        public void Configure(EntityTypeBuilder<ProgressTracker> builder)
        {
            builder.HasOne(P => P.Session)
                 .WithMany(S => S.ProgressTrackers)
                 .HasForeignKey(P => P.SessionId)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(P => P.Patient)
              .WithMany(S => S.ProgressTrackers)
              .HasForeignKey(P => P.PatientId)
              .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
