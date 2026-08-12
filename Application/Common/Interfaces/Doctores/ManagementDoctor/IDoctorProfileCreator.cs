using Application.DTOS.Request.HR.Employee;
using Domain.Entites;
using Domain.Entites.HR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Doctores.ManagementDoctor
{
    public interface IDoctorProfileCreator
    {
        Task CreateAsync(AppUser appUser, Employee employee, HireEmployeeRequest request, CancellationToken ct);
    }
}
