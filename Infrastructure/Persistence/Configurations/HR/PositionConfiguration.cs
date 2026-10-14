using Domain.Entites.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.HR
{
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            #region Table

            builder.ToTable("Positions", table =>
            {
                table.HasCheckConstraint(
                    "CK_Position_BasicSalary",
                    "[BasicSalary] >= 0");

                table.HasCheckConstraint(
                    "CK_Position_Name_NotEmpty",
                    "LEN(LTRIM(RTRIM([Name]))) > 0");              
            });

            #endregion

            #region Properties

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.BasicSalary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.IsActive);

            #endregion

            #region Indexes

            builder.HasIndex(p => new { p.DepartmentId, p.Name })
                .IsUnique();

            #endregion

            #region Relationships

            // Department
            builder.HasOne(p => p.Department)
                .WithMany(d => d.Positions)
                .HasForeignKey(p => p.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Employees
            builder.HasMany(p => p.Employees)
                .WithOne(e => e.Position)
                .HasForeignKey(e => e.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            // Job Postings
            builder.HasMany(p => p.JobPostings)
                .WithOne(j => j.Position)
                .HasForeignKey(j => j.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

          
        }
    }
}
