namespace LDP.Common.Services.Notifications.SMS
{
    public class SendSMSRequest
    {
        public string? SMSMessage { get; set; }
       // public string? FromPhoneNumber { get; set;}

        public string? ToPhoneNumber { get; set;
        
        }
    }

    public class ResplySMSRequest
    {
        public string? SMSMessage { get; set; }
        public string? ToPhoneNumber { get; set; }
        public string? MessageId
        {
            get; set;
        }
    }

    public class GetSMSDetailsRequest
    {
         public string? MessageId { get; set; }
         public string? FromPhoneNumber { get; set; }
         public string?  ToPhoneNumber { get; set; }
         public string? MesageDirection { get; set;}
    }

    public class GetReplyMessageRequest
    {
        public string? MessageId { get; set; }
        
    }
}
