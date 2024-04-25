namespace LDP.Common.BL
{

    public class NotificationDetailModel
    {
        public int NotificationId { get; set; }
        public int OrgId { get; set; }
        public string? NotificationFeature { get; set; }
        public string? NotificationType { get; set; }
        public string? MessageId { get; set; }
        public DateTime? NotificationDate { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? BodyContent { get; set; }
        public DateTime? ReplyDate { get; set; }
        public string? ReplyFrom { get; set; }
        public string? ReplyContent { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? CreatedUser { get; set; }
        public string? ModifiedUser { get; set; }
    }

}
