using Application.Common.Interfaces.Authentication;
using Domain.Entites.HR.Recruitment;
using System.Text;


namespace Infrastructure.services.Authentication
{
    public class InterviewInvitationEmailService : IInterviewInvitationEmailService
    {
        private readonly IEmailService _emailService;
        public InterviewInvitationEmailService(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task SendAsync(Candidate candidate, ApplicationInterview interview, CancellationToken ct)
        {
            var subject = "Welcome to Psychiatric & Addiction Hospital";

            var body = BuildBody(candidate.FullName, interview.Application.JobPosting.Position.Name, interview.Application.JobPosting.Department.Name, interview.ScheduledAt, interview.Location, interview.Interviewer.FullName);

            await _emailService.SendAsync(candidate.Email, subject, body);
        }

        private static string BuildBody(string fullName,string position,string department
            ,DateTime interviewDate,string location,string interviewer)
        {
            var builder = new StringBuilder();

            builder.Append($@"
<html>

<body style='font-family:Segoe UI'>

<h2>Interview Invitation</h2>

<p>
Dear <b>{fullName}</b>,
</p>

<p>
Congratulations! We are pleased to invite you for an interview regarding your application.
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
<b>Location / Meeting Link:</b> {location}
</p>

<p>
<b>Interviewer:</b> {interviewer}
</p>

<p>
</p>

<hr/>

<p>
Please arrive at least <b>15 minutes</b> before your scheduled interview.
</p>

<p>
If you are unable to attend, kindly contact the HR department as soon as possible.
</p>

<p>
We wish you the best of luck!
</p>

<p>
Regards,<br/>
HR Department<br/>
Psychiatric & Addiction Hospital
</p>

</body>

</html>");

            return builder.ToString();
        }

    }
}
