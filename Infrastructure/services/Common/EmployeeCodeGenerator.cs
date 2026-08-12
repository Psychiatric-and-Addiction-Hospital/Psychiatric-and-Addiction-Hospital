using Application.Common.Constants;
using Application.Common.Interfaces.Common;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.Common
{
    public class EmployeeCodeGenerator : IEmployeeCodeGenerator
    {
        private readonly AddIdentityDbContext _context;

        public EmployeeCodeGenerator(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateAsync(string role, CancellationToken ct)
        {
            var year = DateTime.UtcNow.Year;

            var prefix = GetPrefix(role);

            var count = await _context.Employees.CountAsync(
                e => e.EmployeeCode.StartsWith($"{prefix}-{year}-"),
                ct);

            return $"{prefix}-{year}-{(count + 1):D5}";
        }

        private static string GetPrefix(string role)
        {
            return role switch
            {
                Roles.Doctor => EmployeeCodePrefixes.Doctor,

                Roles.Nurse => EmployeeCodePrefixes.Nurse,

                Roles.HR => EmployeeCodePrefixes.HR,

                Roles.Receptionist => EmployeeCodePrefixes.Receptionist,

                Roles.Admin => EmployeeCodePrefixes.Admin,

                _ => EmployeeCodePrefixes.Employee
            };
        }
    }
}

