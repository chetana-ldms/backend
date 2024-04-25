using System.ComponentModel.DataAnnotations;

namespace LDPRuleEngine.BL.Models
{
    public class GetLDPRuleModel: UPdateLDPRuleModelwithoutList
	{
  //   	public int Rule_ID { get; set; }
		//public string? Rule_Name { get; set; }
		//public int Rule_Catagory_ID { get; set; }
		public int active { get; set; }
		public DateTime? CreatedDate { get; set; }
		//public DateTime? Modified_Date { get; set; }
		public string? CreatedUser { get; set; }
		//public string? Modified_User { get; set; }
		public int Processed { get; set; }
		public  List<GetRuleConditionModel>? RuleConditions { get; set; }
	}

	public class CommonLDPRuleModel
	{
		//public int RuleID { get; set; }
		public string? RuleName { get; set; }
		public int RuleCatagoryID { get; set; }

        public string RuleRunAttributeName { get; set; }
        //public int active { get; set; }
        //public DateTime? Created_Date { get; set; }
        //public DateTime? Modified_Date { get; set; }
        //public string? Created_User { get; set; }
        //public string? Modified_User { get; set; }
        //public int Processed { get; set; }

        //public override List<GetRuleConditionModel>? RuleConditions { get; set; }
    }

	public class AddLDPRuleModel: CommonLDPRuleModel
	{
		//public int Rule_ID { get; set; }
		//public string? Rule_Name { get; set; }
		//public int Rule_Catagory_ID { get; set; }
		//public int active { get; set; }
		public DateTime? CreatedDate { get; set; }
		//public DateTime? Modified_Date { get; set; }
		public string? CreatedUser { get; set; }
		//public string? Modified_User { get; set; }
		//public int Processed { get; set; }

		public  List<AddRuleConditionModel>? RuleConditions { get; set; }
	}

	public class UPdateLDPRuleModelwithoutList : CommonLDPRuleModel
	{
		public int RuleID { get; set; }
		//public string? Rule_Name { get; set; }
		//public int Rule_Catagory_ID { get; set; }
		//public int active { get; set; }
		//public DateTime? Created_Date { get; set; }
		public DateTime? ModifiedDate { get; set; }
		//public string? CreatedUser { get; set; }
		public string? ModifiedUser { get; set; }
		//public int Processed { get; set; }

		//public virtual List<UpdateRuleConditionModel>? RuleConditions { get; set; }
	}
	public class UPdateLDPRuleModel: UPdateLDPRuleModelwithoutList
	{
		//public int RuleID { get; set; }
		////public string? Rule_Name { get; set; }
		////public int Rule_Catagory_ID { get; set; }
		////public int active { get; set; }
		////public DateTime? Created_Date { get; set; }
		//public DateTime? ModifiedDate { get; set; }
		////public string? CreatedUser { get; set; }
		//public string? ModifiedUser { get; set; }
		////public int Processed { get; set; }

		public virtual List<UpdateRuleConditionModel>? RuleConditions { get; set; }
	}


}
