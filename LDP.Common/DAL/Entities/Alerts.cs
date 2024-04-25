using System.ComponentModel.DataAnnotations;

namespace LDP_APIs.DAL.Entities
{
	public class Alerts
	{
		[Key]
		public int alert_id { get; set; }
		public double alert_Device_PKID { get; set; }
		public int tool_id { get; set; }
		public int org_id { get; set; }
		public string? name { get; set; }
		public string? severity { get; set; }
		public string? score { get; set; }
		public string? status { get; set; }

        public int status_ID { get; set; }
        public DateTime? detected_time { get; set; }
		public string? observable_tag { get; set; }

		public int observable_tag_ID { get; set; }
		public int owner_user_id { get; set; }

		public string? owner_user_name { get; set; }
		public string? source { get; set; }
		public string? alert_data { get; set; }
		public DateTime? Created_Date { get; set; }
		public DateTime? Modified_Date { get; set; }
		public string? Created_User { get; set; }
		public string? Modified_User { get; set; }
		public int Processed { get; set; }
		public string? Automation_Status { get; set; }
		public int priority_id { get; set; }

		public string? priority_name { get; set; }

		public int positive_analysis_id { get; set; }

		public string? positive_analysis { get; set; }

		public DateTime? resolved_time { get; set; }

		public double resolved_time_Duration { get; set; }

		public int false_positive { get; set; }

		public int alert_incident_mapping_id { get; set; }

        public string? org_tool_severity { get; set; }

        public int severity_id { get; set; }

        public string? severity_name { get; set; }

        public string? event_id { get; set; }

        public string? destination_user { get; set; }

        public string? source_ip { get; set; }

        public string? vendor { get; set; }


    }
}
