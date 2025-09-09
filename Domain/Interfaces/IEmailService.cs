using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IEmailService
    {
        Task SendOtpAsync(string toEmail, string subject, string body);
        bool IsValidEmail(string email);
    }
}
