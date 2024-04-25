using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities.Common
{
    public class AlertHistory
    {
        [Key]
        public int alert_history_id { get; set; }
        public int org_id { get; set; }
        public double alert_id { get; set; }
        public int incident_id { get; set; }
        public DateTime? history_date { get; set; }
        public int created_user_id { get; set; }
        public string? createed_user { get; set; }
        public string? history_description { get; set; }
    }
}
