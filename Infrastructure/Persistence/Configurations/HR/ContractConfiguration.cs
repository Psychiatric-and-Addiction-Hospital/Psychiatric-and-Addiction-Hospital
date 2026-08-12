using Domain.Entites.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.HR
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contracts", table =>
            {
                table.HasCheckConstraint(
                    "CK_Contract_BaseSalary",
                    "[BaseSalary] >= 0");

                table.HasCheckConstraint(
                    "CK_Contract_Dates",
                    "[EndDate] IS NULL OR [EndDate] >= [StartDate]");

                table.HasCheckConstraint(
                    "CK_Contract_Probation",
                    "[ProbationEndDate] IS NULL OR [ProbationEndDate] >= [StartDate]");

                table.HasCheckConstraint(
                    "CK_Contract_SignedDate",
                    "[SignedDate] IS NULL OR [SignedDate] <= [StartDate]");
            });

            #region Properties

            builder.Property(c => c.StartDate)
                .IsRequired();

            builder.Property(c => c.EndDate)
                .IsRequired(false);

            builder.Property(c => c.SignedDate)
                .IsRequired(false);

            builder.Property(c => c.ProbationEndDate)
                .IsRequired(false);

            builder.Property(c => c.BaseSalary)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(c => c.ContractType)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(c => c.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(c => c.Terms)
                .HasMaxLength(4000);

            #endregion

            #region Indexes


            builder.HasIndex(c => c.OfferId)
                .IsUnique();

            builder.HasIndex(c => c.Status);

            builder.HasIndex(c => c.StartDate);

            #endregion

            #region Relationships
            // ApplicationOffer (1) ---- (1) Contract
            builder.HasOne(c => c.Offer)
                .WithOne(o => o.Contract)
                .HasForeignKey<Contract>(c => c.OfferId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
}

