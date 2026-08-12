using Domain.Entites.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.HR
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            #region Table

            builder.ToTable("Departments");

            #endregion

            #region Properties

            builder.Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(d => d.Description)
                .HasMaxLength(1000);

            builder.Property(d => d.IsActive)
                .HasDefaultValue(true);

            #endregion

            #region Indexes

            builder.HasIndex(d => d.Name)
                .IsUnique();

            #endregion

            #region Relationships
            // Department Manager

            builder.HasOne(d => d.Manager)
                .WithMany()
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employees

            builder.HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Positions

            builder.HasMany(d => d.Positions)
                .WithOne(p => p.Department)
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Job Postings

            builder.HasMany(d => d.JobPostings)
                .WithOne(j => j.Department)
                .HasForeignKey(j => j.DepartmentId).OnDelete(DeleteBehavior.Restrict);

            // Hospital Services

            builder.HasMany(d => d.Services)
                .WithOne(s => s.Department)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Constraints

            builder.HasCheckConstraint(
                "CK_Department_Name_NotEmpty",
                "LEN(LTRIM(RTRIM([Name]))) > 0");

            #endregion

        }
    }
}
