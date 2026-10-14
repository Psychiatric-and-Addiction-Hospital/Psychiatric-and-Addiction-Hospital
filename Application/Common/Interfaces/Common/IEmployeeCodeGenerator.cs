using Application.Common.Constants;
using Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Common
{
    public interface IEmployeeCodeGenerator
    {
        Task<string> GenerateAsync(string role, CancellationToken ct);
    }
}
