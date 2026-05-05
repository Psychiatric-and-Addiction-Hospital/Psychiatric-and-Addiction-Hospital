using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Responses;
using Application.DTOS.Responses.HR;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.services.HR.ApplicationInterview
{
    public class CreateApplicationInterviewService : ICreateApplicationInterview
    {
        private readonly AddIdentityDbContext _Context;
        private readonly IEmailService _EmailService;
        public CreateApplicationInterviewService(AddIdentityDbContext context, IEmailService EmailService)
        {
            _Context = context;
            _EmailService = EmailService;
        }

        public async Task<BaseResponse<ApplicationInterviewResponse>> CreateApplicationInterviewAsync(Guid applicationProcessId, DateTime scheduledTime, string interviewerName, InterviewType interviewType, string location, CancellationToken ct)
        {
            var appProcess = await _Context.ApplicationProcesses
        .Include(a => a.Candidate)
        .FirstOrDefaultAsync(a => a.Id == applicationProcessId, ct);

            if (appProcess is null)
                return ResponseFactory.Fail<ApplicationInterviewResponse>("Application process not found.");

            var interview = new Domain.Entites.HR.Applications.ApplicationInterview
            {
                ApplicationProcessId = applicationProcessId,
                ScheduledTime = scheduledTime,
                InterviewerName = interviewerName,
                interviewType = interviewType,
                Location = location
            };

            await _Context.ApplicationInterviews.AddAsync(interview, ct);
            await _Context.SaveChangesAsync(ct);

            string interviewMode = interviewType == InterviewType.Online ? "Online" : "Onsite";
            string locationOrLink = interviewType == InterviewType.Online
                ? $"Online Meeting Link: {location}"
                : $"Location: {location}";

            await _EmailService.SendAsync(
                to: appProcess.Candidate.Email,
                subject: "Your Interview Has Been Scheduled",
                body: $@"
                          Hello {appProcess.Candidate.FullName},
                          Your interview has been successfully scheduled.
                          **Date & Time:** {scheduledTime}
                        **Interviewer:** {interviewerName}
                      **Interview Type:** {interviewMode}
                 📍 **{(interviewType == InterviewType.Online ? "Online Link" : "Location")}:** {location}
               Please make sure to be prepared and available on time.Best regards,HR Team"
            );

           
            return ResponseFactory.Success(new ApplicationInterviewResponse
            {
                Id = interview.Id,
                ApplicationProcessId = interview.ApplicationProcessId,
                ScheduledTime = interview.ScheduledTime,
                InterviewerName = interview.InterviewerName,
                InterviewType = interview.interviewType,
                Location = interview.Location
            },
            "Interview created successfully");
        }
    }
}
