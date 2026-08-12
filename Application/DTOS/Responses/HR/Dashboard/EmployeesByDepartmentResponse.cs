

namespace Application.DTOS.Responses.HR.Dashboard
{
    public class EmployeesByDepartmentResponse
    {
        public string DepartmentName { get; set; } = string.Empty;

        public int EmployeeCount { get; set; }
    }
}
