using Application.Common.Interfaces.Authentication;
using Domain.Entites.HR.Recruitment;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Infrastructure.services.Authentication
{
    public class JobOfferEmailService : IJobOfferEmailService
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        public JobOfferEmailService(IEmailService emailService, IConfiguration configuration)
        {
            _emailService = emailService;
            _configuration = configuration;
        }
        public async Task SendAsync(Candidate candidate, ApplicationOffer offer, CancellationToken ct)
        {
            var subject = "Welcome to Psychiatric & Addiction Hospital";

            var body = BuildBody(candidate.FullName, offer.Application.JobPosting.Position.Name,
                offer.Application.JobPosting.Department.Name, offer.OfferedSalary,
                offer.OfferDate, offer.ExpiryDate, _configuration["CandidatePortalUrl"]);

            await _emailService.SendAsync(candidate.Email, subject, body);
        }
        private static string BuildBody(string fullName, string position, string department,
            decimal salary, DateTime startDate, DateTime expiryDate, string candidatePortalUrl)
        {
            var builder = new StringBuilder();

            builder.Append($@"
<html>

<body style='font-family:Segoe UI'>

<h2>Congratulations!</h2>

<p>
Dear <b>{fullName}</b>,
</p>

<p>
We are delighted to offer you the position of
<b>{position}</b>
at
<b>Psychiatric & Addiction Hospital</b>.
</p>

<hr/>

<p>
<b>Department:</b> {department}
</p>

<p>
<b>Monthly Salary:</b> {salary:N2}
</p>

<p>
<b>Expected Start Date:</b> {startDate:dddd, dd MMMM yyyy}
</p>

<p>
<b>Offer Valid Until:</b> {expiryDate:dddd, dd MMMM yyyy}
</p>

<hr/>

<p>
Please review your employment offer using the link below:
</p>

<p>
<a href='{candidatePortalUrl}'>
Review Your Job Offer
</a>
</p>

<hr/>

<p>
If we do not receive your response before the expiration date,
the offer will automatically expire.
</p>

<p>
We look forward to welcoming you to our team.
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
