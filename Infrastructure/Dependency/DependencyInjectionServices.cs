using Application.Common.Interfaces.Admin;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Doctores.Booking;
using Application.Common.Interfaces.Doctores.ManagementDoctor;
using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Interfaces.HR.ApplicationProcess;
using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Interfaces.HR.Contract;
using Application.Common.Interfaces.HR.Depertment;
using Application.Common.Interfaces.HR.Employee;
using Application.Common.Interfaces.HR.Payroll;
using Application.Common.Interfaces.HR.Recruitment;
using Application.Common.Interfaces.Patient;
using Application.Common.Interfaces.Services;
using Infrastructure.services.Admin.DoctorManagement;
using Infrastructure.services.Authentication;
using Infrastructure.services.Depertment;
using Infrastructure.services.Doctores.Booking;
using Infrastructure.services.Doctores.ManagementDoctor;
using Infrastructure.services.Doctores.Schedule;
using Infrastructure.services.HR.ApplicationInterview;
using Infrastructure.services.HR.ApplicationOffer;
using Infrastructure.services.HR.ApplicationProcess;
using Infrastructure.services.HR.Attendance;
using Infrastructure.services.HR.Candidate;
using Infrastructure.services.HR.Contract;
using Infrastructure.services.HR.Depertment;
using Infrastructure.services.HR.Employees;
using Infrastructure.services.HR.Payroll;
using Infrastructure.services.HR.Recruitment;
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

            #region -------- HR ------
            // Employee services
            services.AddScoped<ICreateEmployee, CreateEmployeeService>();
            services.AddScoped<IGetAllEmployee, GetAllEmpolyee>();
            services.AddScoped<IGetEmployee, GetByIDEmployeeService>();
            services.AddScoped<IUpdateEmployee, UpdateEmployeeService>();
            services.AddScoped<IDeleteEmployee, DeleteEmployeeService>();

            // Attendance
            services.AddScoped<IAttendanceService, AttendanceService>();

            //Depertment
            services.AddScoped<IGetDepertments, GetAllDepertmentService>();
            services.AddScoped<IGetDepartmentById, GetDepartmentByIdService>();
            services.AddScoped<ICreateDepartment, CreateDepartmentService>();
            services.AddScoped<IUpdateDepartment, UpdateDepartmentService>();
            services.AddScoped<IDeleteDepartment, DeleteDepartmentService>();


            // Payroll
            services.AddScoped<IPayrollService, PayrollService>();

            //Recruitment
            services.AddScoped<ICreateRecruitment, CreateRecruitmentService>();
            services.AddScoped<IDeleteRecruitment, DeleteRecruitmentService>();
            services.AddScoped<IUpdateRecruitment, UpdateRecruitmentService>();
            services.AddScoped<IGetAllRecruitment, GetAllRecruitmentService>();

            //ApplicationProcess
            services.AddScoped<ICreateApplicationProcess, CreateApplicationProcessService>();
            services.AddScoped<IUpdateApplicationProcessStatus, UpdateApplicationProcessStatusService>();
            services.AddScoped<IGetAllApplicationProcesses, GetAllApplicationProcessesService>();

            //ApplicationInterview
            services.AddScoped<ICreateApplicationInterview, CreateApplicationInterviewService>();
            services.AddScoped<IUpdateInterviewResult, UpdateInterviewResultService>();

            //ApplicationOffer
            services.AddScoped<ICreateApplicationOffer, CreateApplicationOfferService>();
            services.AddScoped<IUpdateApplicationOfferStatus, UpdateApplicationOfferStatusService>();

            //Candidate
            services.AddScoped<ICreateCandidate, CreateCandidateService>();
            services.AddScoped<IUpdateCandidate, UpdateCandidateService>();
            services.AddScoped<IDeleteCandidate, DeleteCandidateService>();
            services.AddScoped<IGetCandidateById, GetCandidateByIdService>();
            services.AddScoped<IGetAllCandidate, GetAllCandidateService>();

            //Contract
            services.AddScoped<ICreateContract, CreateContractService>();
            services.AddScoped<IUpdateContract, UpdateContractService>();
            services.AddScoped<IDeleteContract, DeleteContractService>();
            services .AddScoped<IGetContractById, GetContractByIdService>();
            services.AddScoped<IGetAllContracts, GetAllContractService>();


            #endregion


            #region---------- Doctores ----------
            //services.AddScoped<IDoctoreApplication, DoctoreApplicationService>();
            services.AddScoped<IGetDoctorSchedules, GetDoctorSchedulesService>();
            services.AddScoped<ICreateDoctorSchedule, CreateDoctorScheduleService>();
            services.AddScoped<IDeleteDoctorSchedule, DeleteDoctorScheduleService>();
            services.AddScoped<IGetAllDoctors, GetAllDoctorsService>();
            services.AddScoped<IGetDoctorById, GetDoctorByIdService>();
            services.AddScoped<IGetDoctorAvailableAppointments, GetDoctorAvailableAppointmentsService>();
            services.AddScoped<IGetDoctorPublicBookings, GetDoctorPublicBookingsService>();
          //  services.AddScoped<IGetDoctorPublicBookingById, GetDoctorPublicBookingByIdService>();   
                        services.AddScoped<IRejectBooking, RejectBookingService>();  
            services.AddScoped<IApproveBooking, ApproveBookingService>(); 

            services.AddScoped<IAdminDoctorManagement, AdminDoctorManagementService>();
            #endregion

            #region --------Patient------
            services.AddScoped<ICreatePublicBooking, CreatePublicBookingService>();
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
