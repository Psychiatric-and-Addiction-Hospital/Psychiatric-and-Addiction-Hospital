namespace Application.DTOS.Request.Doctor
{
    public class DoctorProfileRequest
    {
        public string Degree { get; set; } = string.Empty;

        public string Specialization { get; set; } = string.Empty;

        public string LicenseNumber { get; set; } = string.Empty;

        public string Qualifications { get; set; } = string.Empty;

        public int YearsOfExperience { get; set; }
    }
}
