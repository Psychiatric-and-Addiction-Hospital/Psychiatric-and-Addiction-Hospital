﻿using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class DoctorApplicationConfiguration : IEntityTypeConfiguration<DoctorApplication>
    {
        public void Configure(EntityTypeBuilder<DoctorApplication> builder)
        {
            builder.HasOne(D => D.User)
                 .WithOne(A => A.DoctorApplication)
                 .HasForeignKey<DoctorApplication>(P => P.UserId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
