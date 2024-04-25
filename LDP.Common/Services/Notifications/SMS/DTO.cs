namespace LDP.Common.Services.Notifications.SMS
{
    public class SMSConfiguration
    {
        public string? SID   { get; set; }
        public string? AuthToken { get; set; }

        public string? TwilioPhoneNumber { get; set;}

        public string? URL { get; set;}

    }
}
