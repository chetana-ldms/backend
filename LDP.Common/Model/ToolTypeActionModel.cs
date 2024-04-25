namespace LDP.Common.Model
{
    public class CommonToolTypeActionModel
    {
		public int ToolTypeID { get; set; }
		public string? ToolAction { get; set; }
	}
	public class AddToolTypeActionModel: CommonToolTypeActionModel
	{
		public DateTime? CreatedDate { get; set; }
        //public string? CreatedUser { get; set; }

        public int CreatedUserId { get; set; }
    }
	public class UpdateToolTypeActionModel: CommonToolTypeActionModel
	{
		public int ToolTypeActionID { get; set; }
		public DateTime? ModifiedDate { get; set; }
        //public string? ModifiedUser { get; set; }

        public int ModifiedUserId { get; set; }

    }

	public class GetToolTypeActionModel: UpdateToolTypeActionModel
	{
		public int active { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string? CreatedUser { get; set; }
		public int Processed { get; set; }

		public DateTime? DeletedDate { get; set; }
		public string? DeletedUser { get; set; }
		public string? ToolTypeName { get; set; }
        public string? ModifiedUser { get; set; }
    }

	public class DeleteToolTypeActionModel 
	{
		public int ToolTypeActionID { get; set; }
		public DateTime? DeletedDate { get; set; }
		//public string? DeletedUser { get; set; }

        public int DeletedUserId { get; set; }
    }
}
