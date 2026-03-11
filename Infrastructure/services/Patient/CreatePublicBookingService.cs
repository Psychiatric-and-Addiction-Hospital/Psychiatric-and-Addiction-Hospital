using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Domain.Entites;
using Domain.Entites.ServicesModule;
using Domain.Enums;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.Patient
{
    public class CreatePublicBookingService : ICreatePublicBooking
    {
        private readonly AddIdentityDbContext _Context;
        public CreatePublicBookingService(AddIdentityDbContext context)
        {
            _Context = context;

        }

        public async Task<BaseResponse<PublicBookingResponse>> CreatePublicBooking(
            string fullName, string phoneNumber, string email, Guid doctorId, Guid ScheduleId, string notes, CancellationToken ct)
        {
            var doctor = await _Context.DoctorProfiles
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.Id == doctorId, ct);
            var schedule = await _Context.DoctorSchedules.FirstOrDefaultAsync(s => s.Id == ScheduleId);


            if (doctor == null)
            {
                return ResponseFactory.Fail<PublicBookingResponse>("Doctor not found",
                    new List<string> { "The provided doctorId does not match any existing doctor record." });
            }

            if (schedule == null)
            {
                return ResponseFactory.Fail<PublicBookingResponse>("Schedule not found",
                    new List<string> { "The provided ScheduleId does not match any existing Schedule record." });

            }
            if (schedule.DoctorProfileId != doctorId)
            {
                return ResponseFactory.Fail<PublicBookingResponse>(
                    "Invalid schedule",
                    new List<string> { "This schedule does not belong to the selected doctor." });
            }
            if (schedule.IsBooked)
            {
                return ResponseFactory.Fail<PublicBookingResponse>("Slot already booked",
                new List<string> { "The Selected Time Slot Is alresdy booked ." });
            }
            var Booking = new PublicBooking
            {
                FullName = fullName,
                PhoneNumber = phoneNumber,
                Email = email,
                DoctorId = doctorId,
                PreferredDate = schedule.Date,
                PreferredTime = schedule.Time,
                Notes = notes,
                Status = Status.Pending
            };
            schedule.IsBooked = true;
            await _Context.PublicBookings.AddAsync(Booking, ct);
            await _Context.SaveChangesAsync(ct);

            return ResponseFactory.Success(new PublicBookingResponse
            {
                Id = Booking.Id,
                FullName = Booking.FullName,
                PhoneNumber=Booking.PhoneNumber,
                Email=Booking.Email,
                DoctorId = Booking.DoctorId,
                DoctorName = doctor.User.FirstName,
                PreferredDate = Booking.PreferredDate,
                PreferredTime = Booking.PreferredTime,
                Status = Booking.Status
            }, "Public booking created successfully");
        }
    }
}
