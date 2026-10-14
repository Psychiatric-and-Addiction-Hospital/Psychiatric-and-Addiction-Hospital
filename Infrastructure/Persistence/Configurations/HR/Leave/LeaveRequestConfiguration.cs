using Domain.Entites.HR.Leave;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Persistence.Configurations.HR.Leave
{
    public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder.ToTable("LeaveRequests", table =>
            {
                table.HasCheckConstraint(
                    "CK_LeaveRequest_Dates",
                    "[EndDate] >= [StartDate]");
            });

            #region Properties

            builder.Property(l => l.StartDate)
                .IsRequired();

            builder.Property(l => l.EndDate)
                .IsRequired();

            builder.Property(l => l.Reason)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(l => l.Status)
                .HasConversion<string>()
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(l => l.ManagerComment)
                .HasMaxLength(1000);

            builder.Property(l => l.DecisionDate)
                .IsRequired(false);

            builder.Property(x => x.NumberOfDays)
                .IsRequired();

            #endregion

            #region Indexes

            builder.HasIndex(x => new
            {
                x.EmployeeId,
                x.Status
            });

            builder.HasIndex(x => new
            {
                x.EmployeeId,
                x.StartDate
            });

            builder.HasIndex(l => l.LeaveTypeId);

            builder.HasIndex(l => l.EndDate);

            builder.HasIndex(l => l.ApprovedByEmployeeId);

            #endregion

            #region Relationships

            // Employee who requested leave
            builder.HasOne(l => l.Employee)
                .WithMany(e => e.LeaveRequests)
                .HasForeignKey(l => l.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Leave Type
            builder.HasOne(l => l.LeaveType)
                .WithMany(t => t.LeaveRequests)
                .HasForeignKey(l => l.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Manager / HR who approved
            builder.HasOne(l => l.ApprovedByEmployee)
                .WithMany(e => e.ApprovedLeaveRequests)
                .HasForeignKey(l => l.ApprovedByEmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
        }
    }
}

