using System.ComponentModel;

namespace Application.DTOS.Request.Doctor
{
    public class GetDoctorScheduleListRequest
    {
        public bool Descending { get; set; } = false;

        [DefaultValue(1)]
        public int PageNumber { get; set; } = 1;

        [DefaultValue(10)]
        public int PageSize { get; set; } = 10;
    }
}
