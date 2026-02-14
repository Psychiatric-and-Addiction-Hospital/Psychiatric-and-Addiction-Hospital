namespace Psychiatric_and_Addiction_Hospital.Extesion
{
    public static class AuthorizationExtensions
    {

        //الكود ده بيحدد مين اللى مسموح لهو يدخل ال Endpoint
        public static IServiceCollection AddCustomAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("DoctorOnly", policy => policy.RequireRole("Doctor"));
                options.AddPolicy("PatientOnly", policy => policy.RequireRole("Patient"));
                options.AddPolicy("DoctorOrAdmin", policy => policy.RequireRole("Doctor", "Admin"));
            });
            return services;
        }
    }
}
