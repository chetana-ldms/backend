namespace LDPRuleEngine.BL.Models
{
    public class CommonRuleActionModel
    {
			public string? RuleActionName { get; set; }
			public int ToolTypeID { get; set; }
			public int ToolID { get; set; }
			public int ToolActionID { get; set; }
			public int RuleGenerelActionID { get; set; }

			public int OrgId { get; set; }

    }

	public class AddRuleActionModel: CommonRuleActionModel
	{
		public DateTime? Createddate { get; set; }
		public int CreateduserId { get; set; }
	
	}

	public class UpdateRuleActionModel: CommonRuleActionModel
	{
		public int RuleActionID { get; set; }
		public DateTime? Modifieddate { get; set; }
		
	
	}

	public class GetRuleActionModel: UpdateRuleActionModel
	{
		public int active { get; set; }
		public DateTime? Createddate { get; set; }
		public string? Createduser { get; set; }
		public int Processed { get; set; }
		public DateTime? DeletedDate { get; set; }
		public string? DeletedUser { get; set; }
        public string? Modifieduser { get; set; }
        public string? ToolTypeName { get; set; }
		public string? ToolName { get; set; }
		public string? ToolActionName { get; set; }
        public string? OrgName { get; set; }
        public int OrgId { get; set; }


    }

	public class DeleteRuleActionModel 
	{
		public int RuleActionID { get; set; }
		public DateTime? DeletedDate { get; set; }
		public int DeletedUserId { get; set; }

	}
}
