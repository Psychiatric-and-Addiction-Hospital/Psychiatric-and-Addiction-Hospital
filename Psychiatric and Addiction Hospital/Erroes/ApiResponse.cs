namespace Psychiatric_and_Addiction_Hospital.Erroes
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ApiResponse(int statuscode, string massage = null)
        {
            StatusCode = statuscode;
            Message = massage ?? GetDefaultMassageForStatusCode(statuscode);
        }

        private string GetDefaultMassageForStatusCode(int statuscode)
       => statuscode switch
       {
           400 => "A Bad Request, You Have Made",
           401 => "Authorized, You Are Not",
           404 => "Resource Was Not Found",
           500 => "Errors Are The Path To The Dark Side. ",
           _ => null
       };
    }
}
