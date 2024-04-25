using System;

namespace LDP.Common.Model
{
    public class ApplicationLogModel
    {
        //public int LogId { get; set; }
        //public DateTime? Timestamp { get; set; } 
        public string? Severity { get; set; }
        public int? UserId { get; set; }
        public string? IpAddress { get; set; }
        public string? LogSource { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public string? RequestData { get; set; }
        public string? ResponseData { get; set; }
        public string? AdditionalInfo { get; set; }

        public int? OrgId { get; set; }
    }

    public class GetApplicationLogModel:ApplicationLogModel
    {
        public int LogId { get; set; }
        public DateTime? Timestamp { get; set; }
    }
}
