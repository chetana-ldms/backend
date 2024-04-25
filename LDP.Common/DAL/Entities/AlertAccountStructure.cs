using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities
{
    public class AlertsAccountStructure
    {
        [Key]
        public int? strurcture_id { get; set; }
        public int? org_id { get; set; }
        public int? alert_id { get; set; }
        public string? account_id { get; set; }
        public string? site_id { get; set; }

        public string? threat_id { get; set; }
        
        public string? grouup_id { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public string? created_user { get; set; }
    }

}
