using Domain.Entites.ServicesModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations.Services
{
    public class ServiceConfig : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(s => s.Description)
                   .HasMaxLength(1000);

            builder.HasOne(s => s.ServiceCategory)
                   .WithMany(c => c.Services)
                   .HasForeignKey(s => s.ServiceCategoryId);
        }
    }

}
