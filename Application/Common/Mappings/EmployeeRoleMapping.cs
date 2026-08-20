using Application.Common.Constants;
using Application.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Mappings
{
    public static class EmployeeRoleMapping
    {
        public static string ToIdentityRole(this EmployeeRole role)
        {
            return role switch
            {
                EmployeeRole.Doctor => Roles.Doctor,
                EmployeeRole.Nurse => Roles.Nurse,
                EmployeeRole.Receptionist => Roles.Receptionist,
                EmployeeRole.HR => Roles.HR,
                EmployeeRole.Admin => Roles.Admin,

                _ => throw new ArgumentOutOfRangeException(
                    nameof(role),
                    role,
                    "Invalid employee role.")
            };
        }
    }
}
