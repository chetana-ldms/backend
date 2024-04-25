using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities
{
    public class Incident
    {
		[Key]
		public int Incident_ID { get; set; }
		public string? Description { get; set; }

        public string? incident_subject { get; set; }
        public int Priority { get; set; }
		public int Severity { get; set; }
		public string? Type { get; set; }
		public string? Event_ID { get; set; }
		public string? Destination_User { get; set; }
		public string? Source_IP { get; set; }
		public string? Vendor { get; set; }
		public int Owner { get; set; }
		public int Incident_Status { get; set; }
		public DateTime? Created_Date { get; set; }
		public DateTime? Modified_Date { get; set; }
		public string? Created_User { get; set; }
		public string? Modified_User { get; set; }
		public int Processed { get; set; }

		public string? Priority_Name { get; set; }

		public string? Severity_Name { get; set; }

		public string? Owner_Name { get; set; }

		public string? Incident_Status_Name { get; set; }
		public string? Score { get; set; }

		public DateTime? Incident_Date { get; set; }

        public int type_id { get; set; }

        public int org_id { get; set; }

        public int tool_id { get; set; }

        public int internal_incident { get; set; }

    }
}
