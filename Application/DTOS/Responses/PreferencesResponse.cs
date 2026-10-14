namespace Application.Common.Responses
{
    public class PreferencesResponse
    {
        public bool EmailNotifications { get; set; }
        public bool SmsNotifications   { get; set; }
        public bool AppNotifications   { get; set; }
        public string Language         { get; set; } = "ar";
    }
}
