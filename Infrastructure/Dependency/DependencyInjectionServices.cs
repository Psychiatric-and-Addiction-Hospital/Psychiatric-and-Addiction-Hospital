using Application.Common.Interfaces.Admin;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Depertment;
using Application.Common.Interfaces.Doctores;
using Application.Common.Interfaces.Doctores.ManagementDoctor;
using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.Patient;
using Infrastructure.services.Admin.DoctorManagement;
using Infrastructure.services.Authentication;
using Infrastructure.services.Depertment;
using Infrastructure.services.Doctores;
using Infrastructure.services.Doctores.ManagementDoctor;
using Infrastructure.services.Doctores.Schedule;
using Infrastructure.services.Patient;
using Infrastructure.services.Service;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Dependency
{
    public static class DependencyInjectionServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            #region ---------- Authentication ----------
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IPasswordResetService, PasswordResetService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IVerifyOtp, VerifyOtp>();
            #endregion


            #region---------- Doctores ----------
            services.AddScoped<IDoctoreApplication, DoctoreApplicationService>();
            services.AddScoped<IGetDoctorSchedules, GetDoctorSchedulesService>();
            services.AddScoped<ICreateDoctorSchedule, CreateDoctorScheduleService>();
            services.AddScoped<IDeleteDoctorSchedule, DeleteDoctorScheduleService>();
            services.AddScoped<IGetAllDoctors, GetAllDoctorsService>();
            services.AddScoped<IGetDoctorById, GetDoctorByIdService>();
            services.AddScoped<IGetDoctorAvailableAppointments, GetDoctorAvailableAppointmentsService>();
            services.AddScoped<IGetDoctorPublicBookings, GetDoctorPublicBookingsService>();


            services.AddScoped<IAdminDoctorManagement, AdminDoctorManagementService>();
            #endregion
            #region --------Patient------
            services.AddScoped<ICreatePublicBooking, CreatePublicBookingService>();
            #endregion

            #region --------Depertment------
            services.AddScoped<IGetDepertments, GetAllDepertmentService>();
            services.AddScoped<IGetDepartmentById, GetDepartmentByIdService>();
            services.AddScoped<ICreateDepartment, CreateDepartmentService>();
            services.AddScoped<IUpdateDepartment, UpdateDepartmentService>();
            services.AddScoped<IDeleteDepartment, DeleteDepartmentService>();

            #endregion

            #region -------- Service ------

            services.AddScoped<IGetAllServices, GetAllServicesService>();
            services.AddScoped<IGetServiceById, GetServiceByIdService>();
            services.AddScoped<ICreateService, CreateServiceService>();
            services.AddScoped<IUpdateService, UpdateServiceService>();
            services.AddScoped<IDeleteService, DeleteServiceService>();

            #endregion
        }
    }
}
