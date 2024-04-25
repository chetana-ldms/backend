using System.ComponentModel.DataAnnotations;

namespace LDPRuleEngine.BL.Models
{
	public class CommonRuleConditionModel
	{
		//public int Rules_Condition_ID { get; set; }
		//public int RuleID { get; set; }
		public int RulesConditionTypeID { get; set; }
		//public int active { get; set; }
		//public DateTime? Created_Date { get; set; }
		//public DateTime? Modified_Date { get; set; }
		//public string? Created_User { get; set; }
		//public string? Modified_User { get; set; }
		//public int Processed { get; set; }

       // public List<GetRuleConditionValueModel> RuleConditionValues { get; set; }
    }
	public class AddRuleConditionModel: CommonRuleConditionModel
	{
		//public int Rules_Condition_ID { get; set; }
		//public int Rule_ID { get; set; }
		//public int Rules_Condition_Type_ID { get; set; }
		//public int active { get; set; }
		//public DateTime? CreatedDate { get; set; }
		//public DateTime? Modified_Date { get; set; }
		//public string? CreatedUser { get; set; }
		//public string? Modified_User { get; set; }
		//public int Processed { get; set; }
		public List<AddRuleConditionValueModel> RuleConditionValues { get; set; }
	}
	public class UpdateRuleConditionModelwithoutList : CommonRuleConditionModel
	{
		public int RulesConditionID { get; set; }
		//public int Rule_ID { get; set; }
		//public int Rules_Condition_Type_ID { get; set; }
		//public int active { get; set; }
		//public DateTime? Created_Date { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		//public string? Created_User { get; set; }
		//public string? ModifiedUser { get; set; }
		//public int Processed { get; set; }
		//public List<UpdateRuleConditionValueModel> RuleConditionValues { get; set; }
	}
	public class UpdateRuleConditionModel : UpdateRuleConditionModelwithoutList
	{
		//public int RulesConditionID { get; set; }
		////public int Rule_ID { get; set; }
		////public int Rules_Condition_Type_ID { get; set; }
		////public int active { get; set; }
		////public DateTime? Created_Date { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		////public string? Created_User { get; set; }
		//public string? ModifiedUser { get; set; }
		////public int Processed { get; set; }
		public List<UpdateRuleConditionValueModel> RuleConditionValues { get; set; }
	}

	public class GetRuleConditionModel : UpdateRuleConditionModelwithoutList
	{
		//public int Rules_Condition_ID { get; set; }
		//public int Rule_ID { get; set; }
		//public int Rules_Condition_Type_ID { get; set; }
		//public int active { get; set; }
		public DateTime? CreatedDate { get; set; }
		//public DateTime? Modified_Date { get; set; }
		public string? CreatedUser { get; set; }
		public string? ModifiedUser { get; set; }

		public DateTime? ModifiedDate { get; set; }
		public int Processed { get; set; }
		public List<GetRuleConditionValueModel> RuleConditionValues { get; set; }
	}
}
