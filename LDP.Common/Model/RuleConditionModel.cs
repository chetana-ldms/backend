using System.ComponentModel.DataAnnotations;

namespace LDPRuleEngine.BL.Models
{
	public class CommonRuleConditionModel
	{
		public int RulesConditionTypeID { get; set; }
    }
	public class AddRuleConditionModel: CommonRuleConditionModel
	{
		public List<AddRuleConditionValueModel> RuleConditionValues { get; set; }
	}
	public class UpdateRuleConditionModelwithoutList : CommonRuleConditionModel
	{
		public int RulesConditionID { get; set; }
	}
	public class UpdateRuleConditionModel : UpdateRuleConditionModelwithoutList
	{
		public List<UpdateRuleConditionValueModel> RuleConditionValues { get; set; }
	}

	public class GetRuleConditionModel : UpdateRuleConditionModelwithoutList
	{
		//public DateTime? CreatedDate { get; set; }
		//public string? CreatedUser { get; set; }
		//public string? ModifiedUser { get; set; }

		//public DateTime? ModifiedDate { get; set; }
		//public int Processed { get; set; }
		public List<GetRuleConditionValueModel> RuleConditionValues { get; set; }
		public string?  RulesConditionTypeName { get; set; }

	}
}
