using Application.Common.Interfaces.Doctores.ManagementDoctor;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.services.Doctores.ManagementDoctor
{
    public class GetAllDoctorsService : IGetAllDoctors
    {
        private readonly AddIdentityDbContext _Context;
        public GetAllDoctorsService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<List<DoctorProfileResponse>>> GetAllDoctorsAsync(CancellationToken ct)
        {
            var Profile = await _Context.DoctorProfiles.Include(p => p.Department)
                .Select(p => new DoctorProfileResponse
                {
                    Id = p.Id,
                    FullName = p.FullName,
                    Email = p.Email,
                    PhoneNumber = p.PhoneNumber,
                    Specialization = p.Specialization,
                    Degree = p.Degree,
                    Experience = p.Experience,
                    ImagePath= p.ImagePath,
                    Gender=p.Gender,       
                    DepartmentName = p.Department.Name
                }).ToListAsync(ct);
            if(Profile == null || Profile.Count == 0)
            {
                return ResponseFactory.Fail<List<DoctorProfileResponse>>("No doctors found.");
               
            }
            return ResponseFactory.Success(Profile, "Doctors retrieved successfully.");
        }

    }
}
