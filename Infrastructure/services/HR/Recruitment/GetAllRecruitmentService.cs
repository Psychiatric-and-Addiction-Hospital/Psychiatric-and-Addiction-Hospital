using Application.Common.Interfaces.HR.Recruitment;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.Recruitment
{
    public class GetAllRecruitmentService : IGetAllRecruitment
    {
        private readonly AddIdentityDbContext _Context;
        public GetAllRecruitmentService(AddIdentityDbContext context)
        {
            _Context = context;
        }
        public async Task<BaseResponse<List<RecruitmentResponse>>> GetAllRecruitment(CancellationToken ct)
        {
            var Recruitment = await _Context.Recruitments.Select(r => new RecruitmentResponse
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description,
                MinSalary = r.MinSalary,
                MaxSalary = r.MaxSalary,
                ExperienceLevel = r.ExperienceLevel,
                DepartmentId = r.DepartmentId,
                HiringManagerId = r.HiringManagerId
            }).ToListAsync(ct);
            return ResponseFactory.Success(Recruitment, Recruitment.Any() ? "Recruitment retrieved successfully" : "No Recruitment found");
        }
    }
}
