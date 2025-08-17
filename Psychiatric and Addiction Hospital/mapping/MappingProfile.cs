using Application.DTO;
using AutoMapper;
using Domain.Entites;

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
        }
    }
}
