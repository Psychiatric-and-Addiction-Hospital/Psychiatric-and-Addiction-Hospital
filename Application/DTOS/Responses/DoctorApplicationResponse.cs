using Domain.Enums;
using System;


namespace Application.DTOS.Responses
{
    public class DoctorApplicationResponse
    {
        public Guid ApplicationId { get; set; }
        public string FullName { get; set; }
        public Status Status { get; set; }

    }
}
