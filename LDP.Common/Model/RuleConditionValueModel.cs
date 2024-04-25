using System.ComponentModel.DataAnnotations;

namespace LDPRuleEngine.BL.Models
{
    public class CommonRuleConditionValueModel
	{
		//public int Rules_Condition_Value_ID { get; set; }
		//public int RulesConditionID { get; set; }
		public string? RulesConditionValue { get; set; }
		//public int active { get; set; }
		//public DateTime? Created_Date { get; set; }
		//public DateTime? Modified_Date { get; set; }
		//public string? Created_User { get; set; }
		//public string? Modified_User { get; set; }
		//public int Processed { get; set; }
	}

		public class AddRuleConditionValueModel: CommonRuleConditionValueModel
	{
		//public int Rules_Condition_Value_ID { get; set; }
		//public int Rules_Condition_ID { get; set; }
		//public string? Rules_Condition_Value { get; set; }
		//public int active { get; set; }
		//public DateTime? CreatedDate { get; set; }
		//public DateTime? Modified_Date { get; set; }
		//public string? CreatedUser { get; set; }
		//public string? Modified_User { get; set; }
		//public int Processed { get; set; }
	}

	public class UpdateRuleConditionValueModel: CommonRuleConditionValueModel
	{
		public int RulesConditionValueID { get; set; }
		//public int Rules_Condition_ID { get; set; }
		//public string? Rules_Condition_Value { get; set; }
		//public int active { get; set; }
		//public DateTime? Created_Date { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		//public string? Created_User { get; set; }
		//public string? ModifiedUser { get; set; }
		//public int Processed { get; set; }
	}

	public class GetRuleConditionValueModel : UpdateRuleConditionValueModel
	{
		//public int Rules_Condition_Value_ID { get; set; }
		//public int Rules_Condition_ID { get; set; }
		//public string? Rules_Condition_Value { get; set; }
		//public int active { get; set; }
		//public DateTime? CreatedDate { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		//public string? CreatedUser { get; set; }
		//public string? ModifiedUser { get; set; }
		//public int Processed { get; set; }
	}
}
