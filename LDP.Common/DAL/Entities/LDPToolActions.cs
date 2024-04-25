using System.ComponentModel.DataAnnotations;

namespace LDP_APIs.DAL.Entities
{
    public class LDPToolActions
    {
		[Key]
		public int Tool_Action_ID { get; set; }
		public int Tool_ID { get; set; }
		public int Tool_Type_Action_ID { get; set; }
		public int active { get; set; }
		public DateTime? Created_date { get; set; }
		public DateTime? Modified_date { get; set; }
		public string? Created_user { get; set; }
		public string? Modified_user { get; set; }
		public int Processed { get; set; }

		public DateTime? deleted_date { get; set; }
		public string? deleted_user { get; set; }

	}
}
