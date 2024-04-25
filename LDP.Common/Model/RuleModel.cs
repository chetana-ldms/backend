using System.ComponentModel.DataAnnotations;

namespace LDPRuleEngine.BL.Models
{
    public class GetLDPRuleModel: UPdateLDPRuleModelwithoutList
	{
 		public int active { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string? CreatedUser { get; set; }
		//public int Processed { get; set; }
		public  List<GetRuleConditionModel>? RuleConditions { get; set; }

		public DateTime? DeletedDate { get; set; }
		public string? DeletedUser { get; set; }

		public string? RuleCatagoryName { get; set; }

        public int OrgId { get; set; }

        
        public string? OrgName { get; set; }

        public string? ModifiedUser { get; set; }
    }

	public class CommonLDPRuleModel
	{
		public string? RuleName { get; set; }
		public int RuleCatagoryID { get; set; }
        public string RuleRunAttributeName { get; set; }

        public int OrgId { get; set; }
        

    }

	public class AddLDPRuleModel: CommonLDPRuleModel
	{
		public DateTime? CreatedDate { get; set; }
		public int CreatedUserId { get; set; }
		public  List<AddRuleConditionModel>? RuleConditions { get; set; }
	}

	public class UPdateLDPRuleModelwithoutList : CommonLDPRuleModel
	{
		public int RuleID { get; set; }
		public DateTime? ModifiedDate { get; set; }
		//public int ModifiedUserId { get; set; }
	}
	public class UPdateLDPRuleModel: UPdateLDPRuleModelwithoutList
	{
		public virtual List<UpdateRuleConditionModel>? RuleConditions { get; set; }
	}
	//public class DeleteLDPRuleModelwithoutList
	//{Id
	//	public int RuleID { get; set; }
	//	public DateTime? DeletedDate { get; set; }
	//	public string? DeletedUser { get; set; }
	//}
	public class DeleteLDPRuleModel
	{
		public int RuleID { get; set; }
		public DateTime? DeletedDate { get; set; }
		public int DeletedUserId { get; set; }
	}

}
