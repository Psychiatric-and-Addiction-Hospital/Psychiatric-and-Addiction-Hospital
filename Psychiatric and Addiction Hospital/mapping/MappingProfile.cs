using Application.DTO;
using AutoMapper;
using Domain.Entites;
using System.Numerics;

namespace Psychiatric_and_Addiction_Hospital.mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterDto, AppUser>()
                .AfterMap((src, dest) =>
                {
                    dest.UserName = src.Email?.Split('@')[0];
                });
            CreateMap<AppUser, UserDto>();

            CreateMap<DoctorApplicationCreateDto, DoctorApplication>();
            CreateMap<DoctorApplication, DoctorApplicationResponseDto>();

        }
    }
}
