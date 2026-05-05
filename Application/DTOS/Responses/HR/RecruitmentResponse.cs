using System;


namespace Application.DTOS.Responses.HR
{
    public class RecruitmentResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal MinSalary { get; set; }

        public decimal MaxSalary { get; set; }

        public string ExperienceLevel { get; set; }

        public Guid DepartmentId { get; set; }

        public Guid HiringManagerId { get; set; }
    }
}
