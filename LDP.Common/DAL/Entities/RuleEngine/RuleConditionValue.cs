using System.ComponentModel.DataAnnotations;

namespace LDPRuleEngine.DAL.Entities
{
    public class RuleConditionValue
    {
		[Key]
		public int Rules_Condition_Value_ID { get; set; }
		public int Rules_Condition_ID { get; set; }
		public string? Rules_Condition_Value { get; set; }
		public int active { get; set; }
		public DateTime? Created_Date { get; set; }
		public DateTime? Modified_Date { get; set; }
		public string? Created_User { get; set; }
		public string? Modified_User { get; set; }
		public int Processed { get; set; }
	}
}
