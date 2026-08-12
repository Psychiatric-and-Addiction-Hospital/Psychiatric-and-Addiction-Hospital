using Application.Common.Interfaces.Doctores.ManagementDoctor;
using Application.Common.Responses;
using Application.DTOS.Responses;
using Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

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
            //var Profile = await _Context.DoctorProfiles
            //    .Include(d=>d.Employee)
            //    .FirstOrDefaultAsync(d => d.Employee.Id == d.Employee.Id)
            //    //.Select(p => new DoctorProfileResponse
            //    {
            //        Id = p.Id,
            //        //FullName = p.FullName,
            //        //Email = p.Email,
            //        //PhoneNumber = p.PhoneNumber,
            //        //Specialization = p.Specialization,
            //        //Degree = p.Degree,
            //        //Experience = p.Experience,
            //        //ImagePath= p.ImagePath,
            //        //Gender=p.Gender,       
            //    }).ToListAsync(ct);
            //return ResponseFactory.Success(
            //    //Profile, Profile.Any() ?
            //    "All Doctors retrieved successfully" 
            //    //: "No All Doctors found"
            //    );
            throw new NotImplementedException();
        }

    }
}
