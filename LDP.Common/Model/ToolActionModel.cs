namespace LDP.Common.Model
{
    public class CommonToolActionModel
    {
		public int ToolID { get; set; }
		public int ToolTypeActionID { get; set; }
	}
	public class AddToolActionModel: CommonToolActionModel
	{
		public DateTime? CreatedDate { get; set; }
        //public string? CreatedUser { get; set; }

        public int CreatedUserId { get; set; }


    }
    public class UpdateToolActionModel: CommonToolActionModel
	{
		public int ToolActionID { get; set; }
     	public DateTime? ModifiedDate { get; set; }
        //public string? ModifiedUser { get; set; }

        

    }
	public class GetToolActionModel: UpdateToolActionModel
	{
		public int active { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string? CreatedUser { get; set; }
		public int Processed { get; set; }
		public DateTime? DeletedDate { get; set; }
		public string? DeletedUser { get; set; }

		public string? ToolName { get; set; }
		public string? ToolTypeActionName { get; set; }

		public string? ModifiedUser { get;set; }

        public int ToolTypeId { get; set; }

        public string? ToolTypeName { get; set; }


    }
	public class DeleteToolActionModel 
	{
		public int ToolActionId { get; set; }
		public DateTime? DeletedDate { get; set; }
		//public string? DeletedUser { get; set; }
        public int DeletedUserId { get; set; }
    }

}
