using Application.Common.Extensions;
using Application.Common.Interfaces.Doctores.ManagementDoctor;
using Application.Common.Responses;
using Application.DTOS.Request.Doctor;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.Doctores.ManagementDoctor
{
    public class GetAllDoctorsService : IGetAllDoctors
    {
        private readonly AddIdentityDbContext _context;
        public GetAllDoctorsService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PagedResponse<DoctorProfileResponse>>> GetAllDoctorsAsync(DoctorListRequest request, CancellationToken ct)
        {
            var query = _context.DoctorProfiles
                .AsNoTracking()
                .AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                query = query.Where(x =>
                    x.Employee.FirstName.Contains(search) ||
                    x.Employee.LastName.Contains(search) ||
                    x.Employee.Email.Contains(search) ||
                    x.Specialization.Contains(search) ||
                    x.LicenseNumber.Contains(search));
            }
            // Department Filter
            if (request.DepartmentId.HasValue)
            {
                query = query.Where(x =>
                    x.Employee.DepartmentId == request.DepartmentId.Value);
            }

            // Position Filter
            if (request.PositionId.HasValue)
            {
                query = query.Where(x =>
                    x.Employee.PositionId == request.PositionId.Value);
            }

            // Active Filter
            if (request.IsActive.HasValue)
            {
                query = query.Where(x =>
                    x.Employee.IsActive == request.IsActive.Value);
            }

            // Specialization Filter
            if (!string.IsNullOrWhiteSpace(request.Specialization))
            {
                var specialization = request.Specialization.Trim();

                query = query.Where(x =>
                    x.Specialization.Contains(specialization));
            }

            // Sorting
            query = request.SortBy?.ToLower() switch
            {
                "name" => request.Descending
                    ? query.OrderByDescending(x => x.Employee.FirstName)
                    : query.OrderBy(x => x.Employee.FirstName),

                "specialization" => request.Descending
                    ? query.OrderByDescending(x => x.Specialization)
                    : query.OrderBy(x => x.Specialization),

                "experience" => request.Descending
                    ? query.OrderByDescending(x => x.YearsOfExperience)
                    : query.OrderBy(x => x.YearsOfExperience),

                "department" => request.Descending
                    ? query.OrderByDescending(x => x.Employee.Department.Name)
                    : query.OrderBy(x => x.Employee.Department.Name),

                _ => request.Descending
                    ? query.OrderByDescending(x => x.Employee.FirstName)
                    : query.OrderBy(x => x.Employee.FirstName)
            };

            // Projection
            var responseQuery = query.Select(x => new DoctorProfileResponse
            {
                Id = x.Id,

                EmployeeId = x.EmployeeId,

                FullName = x.Employee.FullName,

                Email = x.Employee.Email,

                PhoneNumber = x.Employee.PhoneNumber,

                Gender = x.Employee.AppUser.Gender,

                ImagePath = x.Employee.AppUser.ImageUrl,

                Specialization = x.Specialization,

                Degree = x.Degree,

                LicenseNumber = x.LicenseNumber,

                YearsOfExperience = x.YearsOfExperience,

                DepartmentId = x.Employee.DepartmentId,

                DepartmentName = x.Employee.Department.Name,

                PositionId = x.Employee.PositionId,

                PositionName = x.Employee.Position.Name,

                IsActive = x.Employee.IsActive
            });


            var pagedResult = await responseQuery.ToPagedResponseAsync(request.PageNumber, request.PageSize, ct);

            return ResponseFactory.Success(pagedResult, "Doctors retrieved successfully.");
        }

    }
}
