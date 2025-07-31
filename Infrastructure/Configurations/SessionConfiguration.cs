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
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.HasOne(s => s.Doctor)
              .WithMany(A=>A.DoctorSessions)
              .HasForeignKey(s => s.DoctorId)
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Patient)
                .WithMany(A=>A.PatientSessions)
                .HasForeignKey(s => s.PatientId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(x => x.CreatedAt)
       .HasDefaultValueSql("GETDATE()");
        }
    }
}