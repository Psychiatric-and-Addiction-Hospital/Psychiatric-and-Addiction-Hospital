using Application.Common.Extensions;
using Application.Common.Interfaces.Patient;
using Application.Common.Responses;
using Application.DTOS.Request.Patient;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.services.Patient
{
    public class GetAllPatientService : IGetAllPatient
    {
        private readonly AddIdentityDbContext _context;
        public GetAllPatientService(AddIdentityDbContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<PagedResponse<PatientProfileResponse>>> GetAllAsync(PatientListRequest request, CancellationToken ct)
        {
            var query = _context.PatientProfiles
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();

                query = query.Where(x =>
                x.AppUser.FirstName.Contains(search) ||
                x.AppUser.LastName.Contains(search) ||
                x.AppUser.Email.Contains(search) ||
                x.PhoneNumber.Contains(search));
            }

            query = request.SortBy?.ToLower() switch
            {
                "name" => request.Descending
                    ? query.OrderByDescending(x => x.AppUser.FirstName)
                    : query.OrderBy(x => x.AppUser.FirstName),

                "DateOfBirth" => request.Descending
                    ? query.OrderByDescending(x => x.DateOfBirth)
                    : query.OrderBy(x => x.DateOfBirth),

                _ => request.Descending
                    ? query.OrderByDescending(x => x.AppUser.FirstName)
                    : query.OrderBy(x => x.AppUser.FirstName)
            };

            var responseQuery = query.Select(x => new PatientProfileResponse
            {
                Id = x.Id,

                UserId = x.AppUser.Id,

                FullName = $"{x.AppUser.FirstName}{x.AppUser.LastName}",

                Email = x.AppUser.Email,

                PhoneNumber = x.PhoneNumber,

                Gender = x.AppUser.Gender.ToString(),

                ImageUrl = x.AppUser.ImageUrl,

                DateOfBirth = x.DateOfBirth,

                MaritalStatus = x.MaritalStatus.ToString(),

                IsEmailConfirmed = x.AppUser.EmailConfirmed,

                IsActived = x.AppUser.IsActive,

            });


            var pagedResult = await responseQuery.ToPagedResponseAsync(request.PageNumber, request.PageSize, ct);

            return ResponseFactory.Success(pagedResult, "patients retrieved successfully.");
        }
    }
}
