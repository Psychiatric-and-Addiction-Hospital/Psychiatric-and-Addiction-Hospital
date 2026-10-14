using Application.Common.Constants;

namespace Psychiatric_and_Addiction_Hospital.Extesion
{
    public static class AuthorizationExtensions
    {

        //الكود ده بيحدد مين اللى مسموح لهو يدخل ال Endpoint
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole(Roles.Admin));

                options.AddPolicy("HROnly", policy => policy.RequireRole(Roles.HR));

                options.AddPolicy("DoctorOnly", policy => policy.RequireRole(Roles.Doctor));

                options.AddPolicy("PatientOnly", policy => policy.RequireRole(Roles.Patient));

                options.AddPolicy("NurseOnly", policy => policy.RequireRole(Roles.Nurse));

                options.AddPolicy("ReceptionistOnly", policy => policy.RequireRole(Roles.Receptionist));

                options.AddPolicy("CandidateOnly", policy => policy.RequireRole(Roles.Candidate));

                options.AddPolicy("HRManagement", policy => policy.RequireRole(Roles.Admin, Roles.HR));

                options.AddPolicy("MedicalStaff", policy => policy.RequireRole(Roles.Doctor, Roles.Nurse));

                options.AddPolicy("DoctorOrAdmin", policy => policy.RequireRole(Roles.Doctor, Roles.Admin));

                options.AddPolicy("AttendanceManagement", policy => policy.RequireRole(Roles.Admin, Roles.HR, Roles.Receptionist));

                options.AddPolicy("HospitalStaff", policy => policy.RequireRole(
                        Roles.Admin, Roles.HR, Roles.Doctor, Roles.Nurse, Roles.Receptionist));
            });
            return services;
        }
    }
}
