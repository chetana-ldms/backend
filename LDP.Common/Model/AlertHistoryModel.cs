namespace LDP.Common.Model
{
    public class AlertHistoryModel
    {
        public int AlertHistoryId { get; set; }
        public int OrgId { get; set; }
        public double AlertId { get; set; }
        public double IncidentId { get; set; }
        public DateTime? HistoryDate { get; set; }
        public int CreatedUserId { get; set; }
        public string? CreatedUser { get; set; }
        public string? HistoryDescription { get; set; }
    }
}
