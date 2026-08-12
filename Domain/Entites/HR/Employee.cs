using Domain.Common;
using Domain.Entites.DoctorsModule;
using Domain.Entites.HR.Leave;
using Domain.Entites.HR.Performance;
using Domain.Entites.HR.Recruitment;
using Domain.Enums;
using Domain.Enums.HR;
using System;
using System.Collections.Generic;


namespace Domain.Entites.HR
{
    public class Employee : BaseEntity
    {
        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}".Trim();
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime HireDate { get; set; }
        public string NationalId { get; set; } = string.Empty;

        // Personal Information
        public DateOnly? DateOfBirth { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactPhone { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string? TerminationReason { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public Guid PositionId { get; set; }
        public Position Position { get; set; } = null!;
        public Guid ShiftId { get; set; }
        public Shift Shift { get; set; } = null!;
        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = null!;
        public Guid? ManagerId { get; set; }

        public Employee? Manager { get; set; }

        public DoctorProfile? DoctorProfile { get; set; }

        public ICollection<Employee> Subordinates { get; set; } = new List<Employee>();

        public ICollection<JobPosting> ManagedJobPostings { get; set; } = new List<JobPosting>();

        public ICollection<ApplicationInterview> InterviewsConducted { get; set; } = new List<ApplicationInterview>();

        public ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

        public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();

        public ICollection<LeaveRequest> LeaveRequests { get; set; } = new List<LeaveRequest>();

        public ICollection<LeaveRequest> ApprovedLeaveRequests { get; set; } = new List<LeaveRequest>();

        public ICollection<PerformanceReview> PerformanceReviews { get; set; } = new List<PerformanceReview>();

        public ICollection<PerformanceReview> ReviewsGiven { get; set; } = new List<PerformanceReview>();

        public ICollection<EmployeeLeaveBalance> LeaveBalances { get; set; } = new List<EmployeeLeaveBalance>();

    }
}
