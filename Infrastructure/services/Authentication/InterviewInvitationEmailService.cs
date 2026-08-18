using Application.Common.Interfaces.Authentication;
using Domain.Entites.HR.Recruitment;
using Microsoft.Extensions.Configuration;
using System.Text;


namespace Infrastructure.services.Authentication
{
    public class InterviewInvitationEmailService : IInterviewInvitationEmailService
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public InterviewInvitationEmailService(IEmailService emailService, IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
        }
        public async Task SendAsync(Candidate candidate, ApplicationInterview interview, CancellationToken ct)
        {
            var subject = "Welcome to Psychiatric & Addiction Hospital";
            var baseUrl = _configuration["Frontend:CandidateInterviewUrl"];

            var interviewUrl = $"{baseUrl}/{interview.Id}";

            var body = BuildBody(
                candidate.FullName
                , interview.Application.JobPosting.Position.Name
                , interview.Application.JobPosting.Department.Name
                , interview.ScheduledAt,
                interview.Location,
                interview.Interviewer.FullName, interviewUrl);

            await _emailService.SendAsync(candidate.Email, subject, body);
        }


        private static string BuildBody(
            string fullName,
            string position,
            string department,
            DateTime interviewDate,
            string? location,
            string interviewer,
            string interviewUrl)
        {
            var builder = new StringBuilder();

            builder.Append($@"
<html>

<body style='font-family:Segoe UI, Arial, sans-serif; color:#1F2937; line-height:1.6'>

    <h2 style='color:#2B3A67;'>
        Interview Invitation
    </h2>

    <p>
        Dear <b>{fullName}</b>,
    </p>

    <p>
        We are pleased to invite you for an interview regarding your application.
    </p>

    <hr/>

    <p>
        <b>Position:</b> {position}
    </p>

    <p>
        <b>Department:</b> {department}
    </p>

    <p>
        <b>Interview Date:</b> {interviewDate:dddd, dd MMMM yyyy}
    </p>

    <p>
        <b>Interview Time:</b> {interviewDate:hh:mm tt}
    </p>

    <p>
        <b>Location:</b> {location ?? "Online"}
    </p>

    <p>
        <b>Interviewer:</b> {interviewer}
    </p>

    <br/>

    <p>
        Your interview has been scheduled successfully.
        You can view the complete interview details from your Candidate Portal.
    </p>

    <p>
        <a href='{interviewUrl}'
           style='
               display:inline-block;
               padding:12px 20px;
               background-color:#2B3A67;
               color:#FFFFFF;
               text-decoration:none;
               border-radius:6px;
               font-weight:bold;
           '>
            View Interview Details
        </a>
    </p>

    <p>
        From the Candidate Portal, you can view your interview details
        and access the online meeting when it is available.
    </p>

    <hr/>

    <p>
        Please arrive at least <b>15 minutes</b> before your scheduled interview.
    </p>

    <p>
        If you are unable to attend, kindly contact the HR department
        as soon as possible.
    </p>

    <p>
        We wish you the best of luck!
    </p>

    <p>
        Regards,<br/>
        <b>HR Department</b><br/>
        Psychiatric & Addiction Hospital
    </p>

</body>

</html>");

            return builder.ToString();
        }
    }
}
