using System.Threading.Tasks;

namespace Application.Common.Interfaces.Authentication
{
    public interface IEmailService
    {
        Task SendAsync(string to, string subject, string body);
    }
}
