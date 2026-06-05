using Application.Common.Interfaces;
using Application.Common.Interfaces.Doctores.Booking;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Application.DTOS.Responses.booking;
using Domain.Entites;
using Domain.Entites.Features;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.services.Doctores.Booking
{
    public class ApproveBookingService : IApproveBooking
    {
        private readonly AddIdentityDbContext _context;
        private readonly INotificationService _notificationService;

        public ApproveBookingService(AddIdentityDbContext context,INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<BaseResponse<ApproveBookingResponse>> ApproveBookingAsync(Guid bookingId, CancellationToken ct)
        {
           
            var booking = await _context.PublicBookings
                .Include(b => b.Doctor)
                    .ThenInclude(d => d.User)
                .Include(b => b.Schedule)
                .FirstOrDefaultAsync(b => b.Id == bookingId, ct);

            if (booking == null)
            {
                return ResponseFactory.Fail<ApproveBookingResponse>("Booking not found",
                    new List<string> { "The provided bookingId does not match any existing booking." });
            }

            if (booking.Status != Status.Pending)
            {
                return ResponseFactory.Fail<ApproveBookingResponse>(
                    $"Booking has already been {booking.Status}",
                    new List<string> { "Only pending bookings can be approved." });
            }

          
            booking.Status = Status.Approved;

            
            if (booking.Schedule != null)
            {
                booking.Schedule.IsBooked = true;
            }


            var session = new Session
            {
                ScheduledDate = booking.PreferredDate,
                DurationMinutes = 60,
                SessionType = SessionType.InPerson,
                CreatedAt = DateTime.UtcNow,
                Status = SessionStatus.Scheduled,
                DoctorId = booking.Doctor.UserId,
                PatientId = booking.Doctor.UserId
            };

            var patientUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == booking.Email, ct);

            if (patientUser != null)
            {
                session.PatientId = patientUser.Id;
            }

            await _context.Sessions.AddAsync(session, ct);

            // Notification services 
            if (patientUser != null)
            {
                string title = "Booking Approved";
                string message = $"Your booking with Dr. {booking.Doctor.User.FirstName} {booking.Doctor.User.LastName} " +
                                 $"on {booking.PreferredDate:yyyy-MM-dd} at {booking.PreferredTime} has been approved.";

                await _notificationService.SendNotificationAsync(
                    patientUser.Id,
                    title,
                    message,
                    NotificationType.BookingApproved,
                    session.Id
                );
            }

            await _context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new ApproveBookingResponse
            {
                BookingId = booking.Id,
                BookingStatus = booking.Status,
                SessionId = session.Id,
                SessionDate = session.ScheduledDate,
                SessionStatus = session.Status,
                DoctorName = $"{booking.Doctor.User.FirstName} {booking.Doctor.User.LastName}",
                PatientName = booking.FullName,
                Message = "Booking approved successfully. Session created."
            }, "Booking approved successfully");
        }
    }
}
