using Application.Common.Interfaces.HR.Recruitment;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Infrastructure.Persistence.Identity;


namespace Infrastructure.services.HR.Recruitment
{
    public class UpdateRecruitmentService : IUpdateRecruitment
    {
        private readonly AddIdentityDbContext _Context;
        public UpdateRecruitmentService(AddIdentityDbContext Context)
        {
            _Context = Context;
        }

        public async Task<BaseResponse<RecruitmentResponse>> UpdateRecruitmentAsync(Guid recruitmentId, string Title, string Description, decimal MinSalary, decimal MaxSalary, string ExperienceLevel, CancellationToken ct)
        {
            var recruitment = await _Context.Recruitments.FindAsync(recruitmentId);
            if (recruitment == null)
                return ResponseFactory.Fail<RecruitmentResponse>("Recruitment not found.");

            recruitment.Title = Title;
            recruitment.Description = Description;
            recruitment.MinSalary = MinSalary;
            recruitment.MaxSalary = MaxSalary;
            recruitment.ExperienceLevel = ExperienceLevel;

            await _Context.SaveChangesAsync(ct);
            return ResponseFactory.Success(
               new RecruitmentResponse
               {
                   Id = recruitment.Id,
                   Title = recruitment.Title,
                   Description = recruitment.Description,
                   MinSalary = recruitment.MinSalary,
                   MaxSalary = recruitment.MaxSalary,
                   ExperienceLevel = recruitment.ExperienceLevel
               },
               "Recruitment Updated Successfully"
               );

        }
    }
}
