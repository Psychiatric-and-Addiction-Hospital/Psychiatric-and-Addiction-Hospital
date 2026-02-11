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
    public class patientProfileConfiguration : IEntityTypeConfiguration<PatientProfile>
    {
        public void Configure(EntityTypeBuilder<PatientProfile> builder)
        {
            builder.HasOne(P => P.User)
                 .WithOne(A=> A.PatientProfile)
                 .HasForeignKey<PatientProfile>(P => P.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
             
        }
    }
}
