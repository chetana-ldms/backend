namespace LDPRuleEngine.BL.Models
{
    public class CommonRuleActionModel
    {
			//public int ruleActionID { get; set; }
			public string? ruleActionName { get; set; }
			public int ToolTypeID { get; set; }
			public int ToolID { get; set; }
			public int ToolActionID { get; set; }
			public int RuleGenerelActionID { get; set; }
			//public int active { get; set; }
			//public DateTime Createddate { get; set; }
			//public DateTime Modifieddate { get; set; }
			//public string? Createduser { get; set; }
			//public string? Modifieduser { get; set; }
			//public int Processed { get; set; }
		
	}

	public class AddRuleActionModel: CommonRuleActionModel
	{
		//public int ruleActionID { get; set; }
		//public string? ruleActionName { get; set; }
		//public int ToolTypeID { get; set; }
		//public int ToolID { get; set; }
		//public int ToolActionID { get; set; }
		//public int RuleGenerelActionID { get; set; }
		//public int active { get; set; }
		public DateTime Createddate { get; set; }
		//public DateTime Modifieddate { get; set; }
		public string? Createduser { get; set; }
		//public string? Modifieduser { get; set; }
		//public int Processed { get; set; }

	}

	public class UpdateRuleActionModel: CommonRuleActionModel
	{
		public int ruleActionID { get; set; }
		//public string? ruleActionName { get; set; }
		//public int ToolTypeID { get; set; }
		//public int ToolID { get; set; }
		//public int ToolActionID { get; set; }
		//public int RuleGenerelActionID { get; set; }
		//public int active { get; set; }
		//public DateTime Createddate { get; set; }
		public DateTime Modifieddate { get; set; }
		//public string? Createduser { get; set; }
		public string? Modifieduser { get; set; }
		//public int Processed { get; set; }

	}

	public class GetRuleActionModel: UpdateRuleActionModel
	{
		//public int ruleActionID { get; set; }
		//public string? ruleActionName { get; set; }
		//public int ToolTypeID { get; set; }
		//public int ToolID { get; set; }
		//public int ToolActionID { get; set; }
		//public int RuleGenerelActionID { get; set; }
		public int active { get; set; }
		public DateTime Createddate { get; set; }
		//public DateTime Modifieddate { get; set; }
		public string? Createduser { get; set; }
		//public string? Modifieduser { get; set; }
		public int Processed { get; set; }

	}
}
