using System.ComponentModel.DataAnnotations;

namespace LDP_APIs.DAL.Entities
{
	public class Alerts
	{
		[Key]
		public int alert_id { get; set; }

		public int alert_Device_PKID { get; set; }
		public int tool_id { get; set; }
		public int org_id { get; set; }
		public string? name { get; set; }
		public string? severity { get; set; }
		public string? score { get; set; }
		public string? status { get; set; }
		public DateTime detected_time { get; set; }
		public string? observable_tag { get; set; }

		public int observable_tag_ID { get; set; }
		public int owner_user_id { get; set; }

		public string? owner_user_name { get; set; }
		public string? source { get; set; }
		public string? alert_data { get; set; }
		public DateTime Created_Date { get; set; }
		//public DateTime Modified_Date { get; set; }
		public string? Created_User { get; set; }
		//public string? Modified_User { get; set; }
		public int Processed { get; set; }
	}
}
