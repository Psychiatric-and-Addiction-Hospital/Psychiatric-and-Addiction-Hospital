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
    public class GetDoctorByIdService : IGetDoctorById
    {
        private readonly AddIdentityDbContext _Context;
        public GetDoctorByIdService(AddIdentityDbContext context)
        {
            _Context = context;
        }

        public async Task<BaseResponse<DoctorProfileResponse>> GetDoctorByIdAsync(Guid Id, CancellationToken ct)
        {
            var Profile = await _Context.DoctorProfiles
               .FirstOrDefaultAsync(p => p.Id == Id, ct);
            if (Profile == null)
            {
                return ResponseFactory.Fail<DoctorProfileResponse>("Doctor not found.");
              
            }
            return ResponseFactory.Success(new DoctorProfileResponse
            {
                Id = Profile.Id,
                FullName = Profile.FullName,
                Email = Profile.Email,
                PhoneNumber = Profile.PhoneNumber,
                Specialization = Profile.Specialization,
                Degree = Profile.Degree,
                Experience = Profile.Experience,
                ImagePath = Profile.ImagePath,
                

            },"Doctor Profile Retrieved Successfully");
        }
    }
}
