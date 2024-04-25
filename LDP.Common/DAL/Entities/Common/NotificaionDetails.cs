using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities.Common
{
    public class NotificaionDetail
    {
        [Key]
        public int notification_id { get; set; }
        public int org_id { get; set; }
        public string? notification_feature { get; set; }
        public string? notification_type { get; set; }
        public string? message_id { get; set; }
        public DateTime? notification_Date { get; set; }
        public string? from { get; set; }
        public string? to { get; set; }
        public string? subject { get; set; }
        public string? body_content { get; set; }
        public DateTime? reply_Date { get; set; }
        public string? reply_from { get; set; }
        public string? reply_content { get; set; }
        public DateTime? Created_Date { get; set; }
        public DateTime? Modified_Date { get; set; }
        public string? Created_User { get; set; }
        public string? Modified_User { get; set; }
    }
}
