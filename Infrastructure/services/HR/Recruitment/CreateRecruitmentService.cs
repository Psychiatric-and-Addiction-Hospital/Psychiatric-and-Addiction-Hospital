using Application.Common.Interfaces.HR.Recruitment;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Azure.Core;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.Recruitment
{
    public class CreateRecruitmentService : ICreateRecruitment
    {
        private readonly AddIdentityDbContext _Context;
        public CreateRecruitmentService(AddIdentityDbContext Context)
        {
            _Context = Context;
        }
        public async Task<BaseResponse<RecruitmentResponse>> CreateRecruitmentAsync(string Title, string Description,
            decimal MinSalary, decimal MaxSalary, string ExperienceLevel, Guid DepartmentId, Guid HiringManagerId, CancellationToken ct)
        {
            var department = await _Context.Departments.FindAsync(DepartmentId);
            if (department is null)
                return ResponseFactory.Fail<RecruitmentResponse>("Department not found.");

            var hiringManager = await _Context.Employees.FindAsync(HiringManagerId);
            if (hiringManager is null)
                return ResponseFactory.Fail<RecruitmentResponse>("Hiring manager not found.");
            var result = new Domain.Entites.HR.Recruitment
            {
                Title = Title,
                Description = Description,
                MinSalary = MinSalary,
                MaxSalary = MaxSalary,
                ExperienceLevel = ExperienceLevel,
                DepartmentId = DepartmentId,
                HiringManagerId = HiringManagerId,

            };
            await _Context.Recruitments.AddAsync(result, ct);
            await _Context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new RecruitmentResponse
            {
                Id = result.Id,
                Title = result.Title,
                Description = result.Description,
                MinSalary = result.MinSalary,
                MaxSalary = result.MaxSalary,
                ExperienceLevel = result.ExperienceLevel,
                DepartmentId = DepartmentId,
                HiringManagerId = HiringManagerId,
            }, "Recruitment created successfully");
        }
    }
}
