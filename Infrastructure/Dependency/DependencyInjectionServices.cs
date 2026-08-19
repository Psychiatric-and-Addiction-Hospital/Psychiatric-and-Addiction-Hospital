using Application.Common.Interfaces;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.BackgroundJobs;
using Application.Common.Interfaces.ChatMessage;
using Application.Common.Interfaces.Common;
using Application.Common.Interfaces.Doctores.Booking;
using Application.Common.Interfaces.Doctores.ManagementDoctor;
using Application.Common.Interfaces.Doctores.Schedule;
using Application.Common.Interfaces.EmployeeProfile;
using Application.Common.Interfaces.HR.Application;
using Application.Common.Interfaces.HR.ApplicationInterview;
using Application.Common.Interfaces.HR.ApplicationOffer;
using Application.Common.Interfaces.HR.Attendance;
using Application.Common.Interfaces.HR.Candidate;
using Application.Common.Interfaces.HR.CandidatePortal;
using Application.Common.Interfaces.HR.Contract;
using Application.Common.Interfaces.HR.Dashboard;
using Application.Common.Interfaces.HR.Depertment;
using Application.Common.Interfaces.HR.Employee;
using Application.Common.Interfaces.HR.JobPosting;
using Application.Common.Interfaces.HR.LeaveRequest;
using Application.Common.Interfaces.HR.LeaveType;
using Application.Common.Interfaces.HR.Manager;
using Application.Common.Interfaces.HR.Position;
using Application.Common.Interfaces.HR.Shift;
using Application.Common.Interfaces.Patient;
using Application.Common.Interfaces.Profile;
using Application.Common.Interfaces.Services;
using Application.Common.Interfaces.Session;
using Infrastructure.services;
using Infrastructure.services.Authentication;
using Infrastructure.services.ChatMessage;
using Infrastructure.services.Common;
using Infrastructure.services.Depertment;
using Infrastructure.services.Doctores.Booking;
using Infrastructure.services.Doctores.ManagementDoctor;
using Infrastructure.services.Doctores.Schedule;
using Infrastructure.services.EmployeeProfile;
using Infrastructure.services.HR.Application;
using Infrastructure.services.HR.ApplicationInterview;
using Infrastructure.services.HR.ApplicationOffer;
using Infrastructure.services.HR.Attendance;
using Infrastructure.services.HR.Candidate;
using Infrastructure.services.HR.CandidatePortal;
using Infrastructure.services.HR.Contract;
using Infrastructure.services.HR.Dashboard;
using Infrastructure.services.HR.Depertment;
using Infrastructure.services.HR.Employee;
using Infrastructure.services.HR.JobPosting;
using Infrastructure.services.HR.LeaveRequest;
using Infrastructure.services.HR.LeaveType;
using Infrastructure.services.HR.Manager;
using Infrastructure.services.HR.Position;
using Infrastructure.services.HR.shift;
using Infrastructure.services.Patient;
using Infrastructure.services.Profile;
using Infrastructure.services.Service;
using Infrastructure.services.Sessions;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Dependency
{
    public static class DependencyInjectionServices
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDataProtection();

            #region Authentication 
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IPasswordResetService, PasswordResetService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IVerifyOtp, VerifyOtp>();
            services.AddScoped<IRegister, RegisterService>();
            services.AddScoped<ICurrentUser, CurrentUserService>();
            services.AddScoped<IPasswordGenerator, PasswordGeneratorService>();
            services.AddScoped<IEmployeeWelcomeEmailService, EmployeeWelcomeEmailService>();
            services.AddScoped<IChangePassword, ChangePasswordService>();
            services.AddScoped<IEmailVerificationService, EmailVerificationService>();
            services.AddScoped<IJobOfferEmailService, JobOfferEmailService>();
            services.AddScoped<IInterviewInvitationEmailService, InterviewInvitationEmailService>();
            services.AddScoped<ICandidateAccountTokenService, CandidateAccountTokenService>();
            services.AddScoped<ICandidateAccountEmailService, CandidateAccountEmailService>();

            #endregion

            #region HR 

            #region Depertment
            services.AddScoped<IGetDepertments, GetAllDepertmentService>();
            services.AddScoped<IGetDepartmentById, GetDepartmentByIdService>();
            services.AddScoped<ICreateDepartment, CreateDepartmentService>();
            services.AddScoped<IUpdateDepartment, UpdateDepartmentService>();
            services.AddScoped<IDeleteDepartment, DeleteDepartmentService>();
            #endregion

            #region position
            services.AddScoped<IGetPositions, GetPositionsService>();
            services.AddScoped<IGetPositionById, GetPositionByIdService>();
            services.AddScoped<ICreatePosition, CreatePositionService>();
            services.AddScoped<IUpdatePosition, UpdatePositionService>();
            services.AddScoped<IDeletePosition, DeletePositionService>();
            #endregion

            #region Shift
            services.AddScoped<IGetShifts, GetShiftsService>();
            services.AddScoped<IGetShiftById, GetShiftByIdService>();
            services.AddScoped<ICreateShift, CreateShiftService>();
            services.AddScoped<IUpdateShift, UpdateShiftService>();
            services.AddScoped<IDeleteShift, DeleteShiftService>();
            #endregion

            #region Attendance

            services.AddScoped<IAttendanceToken, AttendanceTokenService>();
            services.AddScoped<ICheckInAttendance, CheckInAttendanceService>();
            services.AddScoped<IAttendanceCalculator, AttendanceCalculatorService>();
            services.AddScoped<IAttendanceValidation, AttendanceValidationService>();
            services.AddScoped<ICheckOutAttendance, CheckOutAttendanceService>();
            services.AddScoped<IGetTodayAttendance, GetTodayAttendanceService>();
            services.AddScoped<IAttendanceLock, AttendanceLockService>();
            services.AddScoped<ICheckInAttendance, CheckInAttendanceService>();
            services.AddScoped<IGetAttendanceHistory, GetAttendanceHistoryService>();
            services.AddScoped<IManualAttendance, ManualAttendanceService>();
            services.AddScoped<IAutoAbsent, AutoAbsentService>();

            #endregion

            #region Candidate
            services.AddScoped<ICreateCandidate, CreateCandidateService>();
            services.AddScoped<IUpdateCandidate, UpdateCandidateService>();
            services.AddScoped<IDeleteCandidate, DeleteCandidateService>();
            services.AddScoped<IGetCandidateById, GetCandidateByIdService>();
            services.AddScoped<IGetCandidates, GetCandidatesService>();
            services.AddScoped<ICreateCandidateAccount, CreateCandidateAccountService>();
            services.AddScoped<IGetMyCandidateProfile, GetMyCandidateProfileService>();
            services.AddScoped<IUpdateMyCandidateProfile, UpdateMyCandidateProfileService>();
            services.AddScoped<IGetMyOffers, GetMyOffersService>();
            services.AddScoped<IGetMyApplications, GetMyApplicationsService>();
            services.AddScoped<IGetApplicationStatusHistory, GetApplicationStatusHistoryService>();
            services.AddScoped<IApplicationStatusService, ApplicationStatusService>();
            services.AddScoped<ICandidateDashboard, CandidateDashboardService>();
            services.AddScoped<ICandidateInterview, CandidateInterviewService>();
            #endregion

            #region JobPosting
            services.AddScoped<ICloseJobPosting, CloseJobPostingService>();
            services.AddScoped<ICreateJobPosting, CreateJobPostingService>();
            services.AddScoped<IGetJobPostingById, GetJobPostingByIdService>();
            services.AddScoped<IGetJobPostings, GetJobPostingsService>();
            services.AddScoped<IJobPostingValidation, JobPostingValidationService>();
            services.AddScoped<IPublishJobPosting, PublishJobPostingService>();
            services.AddScoped<IUpdateJobPosting, UpdateJobPostingService>();
            #endregion

            #region Application
            services.AddScoped<IGetApplications, GetApplicationsService>();
            services.AddScoped<IGetApplicationById, GetApplicationByIdService>();
            services.AddScoped<IWithdrawApplication, WithdrawApplicationService>();
            services.AddScoped<IApplicationValidation, ApplicationValidationService>();
            services.AddScoped<ICreateApplication, CreateApplicationService>();
            services.AddScoped<IUpdateApplicationStatus, UpdateApplicationStatusService>();
            services.AddScoped<IDeleteApplication, DeleteApplicationService>();
            #endregion

            #region ApplicationInterview
            services.AddScoped<IGetApplicationInterviewById, GetApplicationInterviewByIdService>();
            services.AddScoped<IGetApplicationInterviews, GetApplicationInterviewsService>();
            services.AddScoped<IUpdateApplicationInterview, UpdateApplicationInterviewService>();
            services.AddScoped<IDeleteApplicationInterview, DeleteApplicationInterviewService>();
            services.AddScoped<ICreateApplicationInterview, CreateApplicationInterviewService>();
            services.AddScoped<ICompleteApplicationInterview, CompleteApplicationInterviewService>();
            services.AddScoped<ICancelApplicationInterview, CancelApplicationInterviewService>();
            services.AddScoped<IApplicationInterviewValidation, ApplicationInterviewValidationService>();
            #endregion

            #region ApplicationOffer
            services.AddScoped<ICreateApplicationOffer, CreateApplicationOfferService>();
            services.AddScoped<IUpdateApplicationOffer, UpdateApplicationOfferService>();
            services.AddScoped<IDeleteApplicationOffer, DeleteApplicationOfferService>();
            services.AddScoped<IGetApplicationOfferById, GetApplicationOfferByIdService>();
            services.AddScoped<IGetApplicationOffers, GetApplicationOffersService>();
            services.AddScoped<IAcceptApplicationOffer, AcceptApplicationOfferService>();
            services.AddScoped<IRejectApplicationOffer, RejectApplicationOfferService>();
            services.AddScoped<IApplicationOfferValidation, ApplicationOfferValidationService>();
            #endregion

            #region Contract
            services.AddScoped<IContractValidation, ContractValidationService>();
            services.AddScoped<ICreateContract, CreateContractService>();
            services.AddScoped<ISignContract, SignContractService>();
            services.AddScoped<ISubmitContractForSignature, SubmitContractForSignatureService>();
            services.AddScoped<IUpdateContract, UpdateContractService>();
            #endregion

            #region Employee
            services.AddScoped<IHireEmployee, HireEmployeeService>();
            services.AddScoped<IEmployeeValidation, EmployeeValidationService>();
            services.AddScoped<IHireEmployeeVaildation, HireEmployeeVaildationService>();
            services.AddScoped<IEmployeeCodeGenerator, EmployeeCodeGenerator>();
            services.AddScoped<IUsernameGenerator, UsernameGenerator>();
            services.AddScoped<IGetEmployees, GetEmployeesService>();
            services.AddScoped<IUpdateEmployee, UpdateEmployeeService>();
            services.AddScoped<IGetEmployeeById, GetEmployeeByIdService>();
            services.AddScoped<IDeleteEmployeeValidation, DeleteEmployeeValidationService>();
            services.AddScoped<IDeleteEmployee, DeleteEmployeeService>();
            services.AddScoped<IRestoreEmployee, RestoreEmployeeService>();
            services.AddScoped<IRestoreEmployeeValidation, RestoreEmployeeValidationService>();
            #endregion

            #region LeaveType
            services.AddScoped<ICreateLeaveType, CreateLeaveTypeService>();
            services.AddScoped<IDeleteLeaveType, DeleteLeaveTypeService>();
            services.AddScoped<ILeaveTypeValidation, LeaveTypeValidationService>();
            services.AddScoped<IUpdateLeaveType, UpdateLeaveTypeService>();
            services.AddScoped<IGetleaveTypeById, GetleaveTypeByIdService>();
            services.AddScoped<IGetLeaveTypes, GetLeaveTypesService>();
            services.AddScoped<IRestoreLeaveType, RestoreLeaveTypeService>();
            #endregion

            #region LeaveRequest
            services.AddScoped<ILeaveRequestValidation, LeaveRequestValidationService>();
            services.AddScoped<ICreateLeaveRequest, CreateLeaveRequestService>();

            #endregion

            #region Dashbord
            services.AddScoped<IGetAttendanceDashboard, GetAttendanceDashboardService>();
            services.AddScoped<IGetDashboardSummary, GetDashboardSummaryService>();
            services.AddScoped<IGetEmployeesByDepartment, GetEmployeesByDepartmentService>();
            services.AddScoped<IGetEmployeesByEmploymentStatus, GetEmployeesByEmploymentStatusService>();
            services.AddScoped<IGetRecruitmentDashboard, GetRecruitmentDashboardService>();
            services.AddScoped<IGetHRDashboard, GetHRDashboardService>();
            services.AddScoped<IGetLeaveDashboard, GetLeaveDashboardService>();
            #endregion


            #region 
            services.AddScoped<IAssignDepartmentManager, AssignDepartmentManagerService>();
            services.AddScoped<IChangeDepartmentManager, ChangeDepartmentManagerService>();
            services.AddScoped<IRemoveDepartmentManager, RemoveDepartmentManagerService>();
            #endregion

            #endregion

            #region EmployeeProfile
            services.AddScoped<IGetMyProfile, GetMyProfileService>();
            services.AddScoped<IUpdateMyProfile, UpdateMyProfileService>();
            #endregion

            #region Profile
            services.AddScoped<IChangeProfileImage, ChangeProfileImageService>();
            #endregion

            #region Doctores

            services.AddScoped<IDoctorProfileCreator, DoctorProfileCreatorService>();
            services.AddScoped<IGetDoctorSchedules, GetDoctorSchedulesService>();
            services.AddScoped<ICreateDoctorSchedule, CreateDoctorScheduleService>();
            services.AddScoped<IDeleteDoctorSchedule, DeleteDoctorScheduleService>();
            services.AddScoped<IGetAllDoctors, GetAllDoctorsService>();
            services.AddScoped<IGetDoctorById, GetDoctorByIdService>();
            services.AddScoped<IGetDoctorAvailableAppointments, GetDoctorAvailableAppointmentsService>();
            services.AddScoped<IGetDoctorPublicBookings, GetDoctorPublicBookingsService>();
            services.AddScoped<IRejectBooking, RejectBookingService>();
            services.AddScoped<IApproveBooking, ApproveBookingService>();
            #endregion

            #region --------Patient------
            services.AddScoped<ICreatePublicBooking, CreatePublicBookingService>();

            // Patient Profile
            services.AddScoped<IGetPatientProfile, GetPatientProfileService>();
            services.AddScoped<IUpdatePatientProfile, UpdatePatientProfileService>();
            services.AddScoped<IUploadPatientImage, UploadPatientImageService>();

            // Patient Management
            services.AddScoped<IGetPatientSessions, GetPatientSessionsService>();
            services.AddScoped<IGetSessionDetails, GetSessionDetailsService>();
            services.AddScoped<IAddSessionNote, AddSessionNoteService>();
            services.AddScoped<IGetPatientDashboard, GetPatientDashboardService>();
            #endregion


            #region -------- Service ------

            services.AddScoped<IGetAllServices, GetAllServicesService>();
            services.AddScoped<IGetServiceById, GetServiceByIdService>();
            services.AddScoped<ICreateService, CreateServiceService>();
            services.AddScoped<IUpdateService, UpdateServiceService>();
            services.AddScoped<IDeleteService, DeleteServiceService>();

            #endregion

            #region Session
            services.AddScoped<ISessionService, SessionService>();
            #endregion

            #region Notification
            services.AddScoped<INotificationService, NotificationService>();
            #endregion

            #region Message
            services.AddScoped<IChatMessage, ChatMessageService>();
            services.AddScoped<IGetConversation, GetConversationServic>();
            #endregion

        }
    }
}
