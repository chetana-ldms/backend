using System.ComponentModel.DataAnnotations;

namespace LDPRuleEngine.DAL.Entities
{
    public class RuleCondition
    {
		[Key]
		public int Rules_Condition_ID { get; set; }
		public int Rule_ID { get; set; }
		public int Rules_Condition_Type_ID { get; set; }
		public int active { get; set; }
		public DateTime? Created_Date { get; set; }
		public DateTime? Modified_Date { get; set; }
		public string? Created_User { get; set; }
		public string? Modified_User { get; set; }
		public int Processed { get; set; }
	}
}
