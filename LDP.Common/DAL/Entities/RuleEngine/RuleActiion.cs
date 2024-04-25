using System.ComponentModel.DataAnnotations;

namespace LDPRuleEngine.DAL.Entities
{
	public class RuleAction
	{
		[Key]
		public int rule_Action_ID { get; set; }
		public string? rule_Action_Name { get; set; }
		public int Tool_Type_ID { get; set; }
		public int Tool_ID { get; set; }
		public int Tool_Action_ID { get; set; }
		public int Rule_Generel_Action_ID { get; set; }
		public int active { get; set; }
		public DateTime? Created_date { get; set; }
		public DateTime? Modified_date { get; set; }
		public string? Created_user { get; set; }
		public string? Modified_user { get; set; }
		public int Processed { get; set; }

		public DateTime? deleted_date { get; set; }
		public string? deleted_user { get; set; }

        public int org_id { get; set; }
    }
}
