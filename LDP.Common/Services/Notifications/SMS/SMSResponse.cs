using LDP_APIs.Models;

namespace LDP.Common.Services.Notifications.SMS
{
    public class SendSMSResponse:baseResponse
    {
        public string? MessageId { get; set; }
    }
    public class ResplySMSResponse : baseResponse
    {
        public string? MessageId { get; set; }
    }
    public class GetSMSDataResponse : baseResponse
    {
        public string? MessageSid { get; set; }
        public string? MessageDirection { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Content { get; set; }

    }

}
