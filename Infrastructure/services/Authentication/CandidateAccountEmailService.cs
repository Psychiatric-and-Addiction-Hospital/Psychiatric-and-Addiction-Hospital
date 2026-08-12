using Application.Common.Interfaces.Authentication;


namespace Infrastructure.services.Authentication
{
    public class CandidateAccountEmailService : ICandidateAccountEmailService
    {
        private readonly IEmailService _emailService;

        public CandidateAccountEmailService(
            IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendAsync(string email, string fullName, string accountSetupUrl, CancellationToken ct)
        {
            var subject =
                "Create your Candidate Account - Psychiatric & Addiction Hospital";

            var body = BuildBody(fullName,accountSetupUrl);

            await _emailService.SendAsync(email, subject, body);
        }

        private static string BuildBody(
            string fullName,
            string accountSetupUrl)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
</head>

<body style='margin:0;
             padding:0;
             background:#f5f6fa;
             font-family:Arial,sans-serif;'>

    <div style='max-width:600px;
                margin:40px auto;
                background:#ffffff;
                border-radius:10px;
                overflow:hidden;'>

        <div style='background:#2B3A67;
                    padding:30px;
                    text-align:center;
                    color:white;'>

            <h1>
                Psychiatric & Addiction Hospital
            </h1>

        </div>

        <div style='padding:35px;
                    color:#333333;'>

            <h2 style='color:#2B3A67;'>
                Welcome, {fullName}!
            </h2>
  <p style='font-size:16px;
                      line-height:1.7;'>

                Your candidate profile has been
                created successfully.

            </p>

            <p style='font-size:16px;
                      line-height:1.7;'>

                Create your Candidate Portal account
                to track your applications, offers,
                and recruitment process.

            </p>

            <div style='text-align:center;
                        margin:30px 0;'>

                <a href='{{accountSetupUrl}}'
                   style='display:inline-block;
                          padding:14px 30px;
                          background:#2B3A67;
                          color:#ffffff;
                          text-decoration:none;
                          border-radius:6px;
                          font-size:16px;
                          font-weight:bold;'>

                    Create Candidate Account

                </a>

            </div>
 <p style='font-size:14px;
                      color:#777777;'>

                This link will expire after
                24 hours.

            </p>

            <p style='margin-top:30px;'>

                Best regards,<br>

                <strong>
                    Psychiatric & Addiction Hospital
                </strong>

            </p>

        </div>

    </div>

</body>
</html>";
        }
    }
}
