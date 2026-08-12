using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Authentication
{
    public interface IEmployeeWelcomeEmailService
    {
        Task SendAsync(string email, string fullName, string employeeCode, string position, string department, string loginUrl, CancellationToken ct);
    }
}
