using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.ApplicationProcess;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.HR.ApplicationProcess
{
    public class CreateApplicationProcessService : ICreateApplicationProcess
    {
        private readonly AddIdentityDbContext _Context;
        private readonly IEmailService _Email;
        public CreateApplicationProcessService(AddIdentityDbContext Context, IEmailService Email)
        {
            _Context = Context;
            _Email = Email;
        }

        public async Task<BaseResponse<ApplicationProcessResponse>> CreateApplicationProcessAsync(Guid CandidateId, Guid RecruitmentId, CancellationToken ct)
        {
            var candidate = await _Context.Candidates.FirstOrDefaultAsync(c => c.Id == CandidateId, ct);

            if (candidate is null)
                return ResponseFactory.Fail<ApplicationProcessResponse>("Candidate not found");
            
            var recruitmentExists = await _Context.Recruitments
                .AnyAsync(r => r.Id == RecruitmentId, ct);

            if (!recruitmentExists)
                return ResponseFactory.Fail<ApplicationProcessResponse>("Recruitment not found");

            var alreadyApplied = await _Context.ApplicationProcesses
                .AnyAsync(p => p.CandidateId == CandidateId && p.RecruitmentId == RecruitmentId, ct);

            if (alreadyApplied)
                return ResponseFactory.Fail<ApplicationProcessResponse>("Candidate already applied for this recruitment");

            var applicationProcess = new Domain.Entites.HR.Applications.ApplicationProcess
            {
                CandidateId = CandidateId,
                RecruitmentId = RecruitmentId,
                States = ApplicationStatus.Pending
            };

            await _Context.ApplicationProcesses.AddAsync(applicationProcess, ct);
            await _Context.SaveChangesAsync(ct);

            await _Email.SendAsync(
                to: candidate.Email, 
                subject: "Application Submitted",
                body: $"Hello {candidate.FullName}, your application has been successfully submitted."
            );

            return ResponseFactory.Success(new ApplicationProcessResponse
            {
                Id = applicationProcess.Id,
                CandidateId = applicationProcess.CandidateId,
                RecruitmentId = applicationProcess.RecruitmentId,
            },
            "Application process initialized successfully");
        }



    }
}

