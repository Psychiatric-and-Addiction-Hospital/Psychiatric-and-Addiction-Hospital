using Application.Commands.HR.JobPosting;
using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Responses;
using Application.DTOS.Responses.HR.JobPosting;
using Domain.Enums.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.HR.JobPosting
{
    public class CreateJobPostingService : ICreateJobPosting
    {
        private readonly AddIdentityDbContext _context;
        private readonly IJobPostingValidation _validation;
        public CreateJobPostingService(AddIdentityDbContext context, IJobPostingValidation validation)
        {
            _context = context;
            _validation = validation;
        }

        public async Task<BaseResponse<JobPostingResponse>> CreateAsync(
            CreateJobPostingCommand request,
            CancellationToken ct)
        {
            //-------------------------------------
            // Validation
            //-------------------------------------

            var validation = await _validation
                .ValidateCreateAsync(request.Request, ct);

            if (!validation.Success)
            {
                return ResponseFactory.Fail<JobPostingResponse>(
                    validation.Message,
                    validation.Errors);
            }

            //-------------------------------------
            // Create Entity
            //-------------------------------------

            var jobPosting = new Domain.Entites.HR.Recruitment.JobPosting
            {
                Title = request.Request.Title.Trim(),

                Description = request.Request.Description.Trim(),

                Location = request.Request.Location.Trim(),

                MinSalary = request.Request.MinSalary,

                MaxSalary = request.Request.MaxSalary,

                Vacancies = request.Request.Vacancies,

                WorkMode = request.Request.WorkMode,

                EmploymentType = request.Request.EmploymentType,

                ExperienceLevel = request.Request.ExperienceLevel,

                PublishedDate = request.Request.PublishedDate,

                ClosingDate = request.Request.ClosingDate,

                Status = JobPostingStatus.Draft,

                DepartmentId = request.Request.DepartmentId,

                PositionId = request.Request.PositionId,

                HiringManagerId = request.Request.HiringManagerId
            };

            _context.JobPostings.Add(jobPosting);

            await _context.SaveChangesAsync(ct);

            //-------------------------------------
            // Load Navigation Properties
            //-------------------------------------

            var createdJobPosting = await _context.JobPostings
               .AsNoTracking()
               .Include(x => x.Department)
               .Include(x => x.Position)
               .Include(x => x.HiringManager)
               .FirstAsync(x => x.Id == jobPosting.Id, ct);

            //-------------------------------------
            // Response
            //-------------------------------------

            var response = new JobPostingResponse
            {
                Id = createdJobPosting.Id,

                Title = createdJobPosting.Title,

                Description = createdJobPosting.Description,

                Location = createdJobPosting.Location,

                MinSalary = createdJobPosting.MinSalary,

                MaxSalary = createdJobPosting.MaxSalary,

                Vacancies = createdJobPosting.Vacancies,

                WorkMode = createdJobPosting.WorkMode,

                EmploymentType = createdJobPosting.EmploymentType,

                ExperienceLevel = createdJobPosting.ExperienceLevel,

                PublishedDate = createdJobPosting.PublishedDate,

                ClosingDate = createdJobPosting.ClosingDate,

                Status = createdJobPosting.Status,

                DepartmentId = createdJobPosting.DepartmentId,

                DepartmentName = createdJobPosting.Department.Name,

                PositionId = createdJobPosting.PositionId,

                PositionName = createdJobPosting.Position.Name,

                HiringManagerId = createdJobPosting.HiringManagerId,

                HiringManagerName = createdJobPosting.HiringManager.FullName
            };

            return ResponseFactory.Success(response, "Job posting created successfully.");
        }

    }
}
