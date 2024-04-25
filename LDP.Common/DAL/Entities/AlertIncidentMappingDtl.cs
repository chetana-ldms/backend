using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities
{
    public class AlertIncidentMappingDtl
    {
        [Key]
        public int alert_incident_mapping_dtl_id { get; set; }
        public double alert_id { get; set; }
        public int alert_incident_mapping_id { get; set; }
    }
}
