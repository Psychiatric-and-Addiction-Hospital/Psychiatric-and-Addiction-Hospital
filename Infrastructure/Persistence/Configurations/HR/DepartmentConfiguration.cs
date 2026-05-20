using Domain.Entites.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.HR
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasIndex(d => d.Name)
                .IsUnique();

            builder.Property(d => d.Description)
                .HasMaxLength(1000)
                .IsRequired(false);

   
            builder.HasMany(d => d.Employees)
                .WithOne(e => e.Department) 
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

             builder.HasOne<Employee>()
                .WithMany() 
                .HasForeignKey(d => d.ManagerId).IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

           
            builder.HasMany(d => d.Recruitments)
                .WithOne(r => r.Department)
                .HasForeignKey(r => r.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

          
            builder.HasMany(d => d.Services)
                .WithOne(s => s.Department)
                .HasForeignKey(s => s.DepartmentId) 
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}