using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Responses;
using Application.DTOS.Request.HR.JobPosting;
using Domain.Entites.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using jobPostingEntity = Domain.Entites.HR.Recruitment.JobPosting;
using employeeEntity = Domain.Entites.HR.Employee;

namespace Infrastructure.services.HR.JobPosting
{
    public class JobPostingValidationService : IJobPostingValidation
    {
        private readonly AddIdentityDbContext _context;

        public JobPostingValidationService(
            AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<bool>> ValidateCreateAsync(CreateJobPostingRequest request, CancellationToken ct)
        {
            if (!await DepartmentExists(request.DepartmentId, ct))
                return ResponseFactory.Fail<bool>("Department not found.");

            if (!await PositionExists(request.PositionId, ct))
                return ResponseFactory.Fail<bool>("Position not found.");

            var dateValidation = ValidateDates(request.PublishedDate, request.ClosingDate);
            if (dateValidation != null)
                return ResponseFactory.Fail<bool>(dateValidation);

            var salaryValidation = ValidateSalary(request.MinSalary, request.MaxSalary);
            if (salaryValidation != null)
                return ResponseFactory.Fail<bool>(salaryValidation);

            var vacanciesValidation = ValidateVacancies(request.Vacancies);
            if (vacanciesValidation != null)
                return ResponseFactory.Fail<bool>(vacanciesValidation);

            return ResponseFactory.Success(true, "Validation succeeded.");
        }
        public async Task<BaseResponse<jobPostingEntity>> ValidateUpdateAsync(UpdateJobPostingRequest Request, CancellationToken ct)
        {
            var jobPosting = await _context.JobPostings.FirstOrDefaultAsync(x => x.Id == Request.Id, ct);

            if (jobPosting == null)
                return ResponseFactory.Fail<jobPostingEntity>("Job posting not found.");

            if (!await DepartmentExists(Request.DepartmentId, ct))
                return ResponseFactory.Fail<jobPostingEntity>("Department not found.");

            if (!await PositionExists(Request.PositionId, ct))
                return ResponseFactory.Fail<jobPostingEntity>("Position not found.");

            var dateValidation = ValidateDates(Request.PublishedDate, Request.ClosingDate);
            if (dateValidation != null)
                return ResponseFactory.Fail<jobPostingEntity>(dateValidation);

            var salaryError = ValidateSalary(Request.MinSalary, Request.MaxSalary);

            if (salaryError != null)
                return ResponseFactory.Fail<jobPostingEntity>(salaryError);

            var vacanciesValidation = ValidateVacancies(Request.Vacancies);

            if (vacanciesValidation != null)
                return ResponseFactory.Fail<jobPostingEntity>(vacanciesValidation);


            return ResponseFactory.Success(jobPosting, "Validation succeeded.");
        }
        public async Task<BaseResponse<jobPostingEntity>> ValidateStatusChangeAsync(Guid jobPostingId, CancellationToken ct)
        {
            var jobPosting = await _context.JobPostings.FirstOrDefaultAsync(x => x.Id == jobPostingId, ct);

            if (jobPosting == null)
                return ResponseFactory.Fail<jobPostingEntity>("Job posting not found.");

            return ResponseFactory.Success(jobPosting, "Validation succeeded.");
        }


        private async Task<bool> DepartmentExists(Guid departmentId, CancellationToken ct)
        {
            return await _context.Departments.AnyAsync(x => x.Id == departmentId, ct);
        }

        private async Task<bool> PositionExists(Guid positionId, CancellationToken ct)
        {
            return await _context.Positions.AnyAsync(x => x.Id == positionId, ct);
        }

        private async Task<employeeEntity?> GetHiringManager(Guid managerId, CancellationToken ct)
        {
            return await _context.Employees.FirstOrDefaultAsync(x => x.Id == managerId, ct);
        }

        private string? ValidateSalary(decimal minSalary, decimal maxSalary)
        {
            if (minSalary < 0)
                return "Minimum salary cannot be negative.";

            if (maxSalary < minSalary)
                return "Maximum salary must be greater than or equal to minimum salary.";

            return null;
        }

        private string? ValidateDates(DateTime publishedDate, DateTime closingDate)
        {
            if (closingDate < publishedDate)
                return "Closing date must be after published date.";

            return null;
        }

        private string? ValidateVacancies(int vacancies)
        {
            if (vacancies <= 0)
                return "Vacancies must be greater than zero.";

            return null;
        }
    }
}
