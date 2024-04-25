using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDP.Common.DAL.Entities
{
    public class AlertIncidentMapping
    {
		[Key]
		public int alert_incident_mapping_id { get; set; }
		public int org_id { get; set; }
		public int tool_type_id { get; set; }
		public int tool_id { get; set; }
		//public double alert_id { get; set; }
		public double incident_number { get; set; }
		public string? incident_data { get; set; }
		public DateTime? Created_Date { get; set; }
		public DateTime? Modified_Date { get; set; }
		public string? Created_User { get; set; }
		public string? Modified_User { get; set; }
		public int Processed { get; set; }
        public int significant_incident { get; set; }

        public string? client_tool_incident_id { get; set; }


    }
}
