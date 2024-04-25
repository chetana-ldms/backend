namespace LDP.Common.Services.Notifications.Mail
{
    public class EmailRequest
    {
        public String? subject { get; set; }
        public string? plainTextContent { get; set; }
        public string? htmlContent { get; set; }
        public string? toMail { get; set; }
        public string? fromMail { get; set; }
        public string? CCs { get; set; }
        public string? BCCs { get; set; }
        public Dictionary<string, string>? Data { get; set; }
    }
}
