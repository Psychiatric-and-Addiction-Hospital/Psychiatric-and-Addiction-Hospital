using Application.Common.Interfaces.Authentication;
using System.Text;

namespace Infrastructure.services.Authentication
{
    public class EmployeeWelcomeEmailService : IEmployeeWelcomeEmailService
    {

        private readonly IEmailService _emailSender;

        public EmployeeWelcomeEmailService(IEmailService emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task SendAsync(string email, string fullName, string employeeCode, string position, string department, string loginUrl, CancellationToken ct)
        {
            var subject = "Welcome to Psychiatric & Addiction Hospital";

            var body = BuildBody(fullName, employeeCode, position, department, loginUrl);

            await _emailSender.SendAsync(email, subject, body);
        }
        private static string BuildBody(string fullName, string employeeCode, string position, string department, string loginUrl)
        {
            var builder = new StringBuilder();

            builder.Append($@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Welcome</title>
</head>

<body style=""
    margin:0;
    padding:0;
    background-color:#f4f6f8;
    font-family:Arial,Helvetica,sans-serif;
"">

    <div style=""
        max-width:600px;
        margin:40px auto;
        background:#ffffff;
        border-radius:10px;
        overflow:hidden;
        box-shadow:0 4px 12px rgba(0,0,0,0.08);
    "">

        <!-- Header -->
        <div style=""
            background-color:#2B3A67;
            padding:30px;
            text-align:center;
            color:#ffffff;
        "">
            <h1 style=""
                margin:0;
                font-size:26px;
            "">
                Psychiatric & Addiction Hospital
            </h1>
        </div>

        <!-- Content -->
        <div style=""
            padding:35px;
            color:#333333;
        "">

            <h2 style=""
                margin-top:0;
                color:#2B3A67;
            "">
                Congratulations, {fullName}!
            </h2>

            <p style=""
                font-size:16px;
                line-height:1.7;
            "">
                We are pleased to inform you that you have been
                officially hired and are now a member of our team.
            </p>

            <p style=""
                font-size:16px;
                line-height:1.7;
            "">
                Your Candidate account has been successfully
                upgraded to an employee account.
            </p>

            <!-- Employee Information -->
            <div style=""
                margin:25px 0;
                padding:20px;
                background-color:#f7f8fb;
                border-left:4px solid #2B3A67;
            "">

                <h3 style=""
                    margin-top:0;
                    color:#2B3A67;
                "">
                    Employment Information
                </h3>

                <p>
                    <strong>Employee Code:</strong>
                    {employeeCode}
                </p>

                <p>
                    <strong>Position:</strong>
                    {position}
                </p>

                <p>
                    <strong>Department:</strong>
                    {department}
                </p>

            </div>

            <p style=""
                font-size:16px;
                line-height:1.7;
            "">
                You can continue using your existing account to
                access the hospital system.
            </p>

            <p style=""
                font-size:16px;
                line-height:1.7;
            "">
                You do <strong>not</strong> need to create a new account
                or password.
            </p>

            <!-- Login Button -->
            <div style=""
                text-align:center;
                margin:30px 0;
            "">

                <a href=""{loginUrl}""
                   style=""
                       display:inline-block;
                       padding:14px 30px;
                       background-color:#2B3A67;
                       color:#ffffff;
                       text-decoration:none;
                       border-radius:6px;
                       font-size:16px;
                       font-weight:bold;
                   "">
                    Login to Your Account
                </a>

            </div>

            <p style=""
                font-size:14px;
                color:#666666;
                line-height:1.6;
            "">
                Please use the same email address and password
                you used for your Candidate account.
            </p>

            <p style=""
                margin-top:30px;
                font-size:16px;
            "">
                We are happy to have you with us.
            </p>

            <p style=""
                font-size:16px;
            "">
                Best regards,<br>
                <strong>Psychiatric & Addiction Hospital</strong>
            </p>

        </div>

        <!-- Footer -->
        <div style=""
            padding:20px;
            text-align:center;
            background-color:#f7f7f7;
            color:#888888;
            font-size:12px;
        "">
            © Psychiatric & Addiction Hospital
        </div>

    </div>

</body>
</html>");
            return builder.ToString();

        }
    }
}