using employeeEntity = Domain.Entites.HR.Employee;

namespace Infrastructure.Persistence.Extensions
{
    public class EmployeeWithRole
    {
        public employeeEntity Employee { get; set; } = null!;

        public string Role { get; set; } = string.Empty;
    }
}
