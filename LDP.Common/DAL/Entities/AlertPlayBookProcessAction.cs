using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities
{
    public class AlertPlayBookProcessAction
    {
		[Key]
		public int alert_playbooks_process_action_id { get; set; }
		public int alert_playbooks_process_id { get; set; }
		public int tool_type_id { get; set; }
		public int tool_id { get; set; }
		public int play_book_id { get; set; }
		public int tool_action_id { get; set; }
		public string? tool_action_status { get; set; }
		public DateTime? Created_Date { get; set; }
		public DateTime? Modified_Date { get; set; }
		public string? Created_User { get; set; }
		public string? Modified_User { get; set; }
		public int Processed { get; set; }
	}
}
