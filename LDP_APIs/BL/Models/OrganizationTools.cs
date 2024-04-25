namespace LDP_APIs.BL.Models
{
    public class CommonOrganizationTools
    {
		//public int Org_Tool_ID { get; set; }
		public int ToolID { get; set; }
		public int OrgID { get; set; }
		public string? AuthKey { get; set; }
		public double LastReadPKID { get; set; }
		public string? ApiUrl { get; set; }

		//public DateTime Created_Date { get; set; }
		//public DateTime Modified_Date { get; set; }
		//public string Created_User { get; set; }
		//public string Modified_User { get; set; }

	}
	public class AddOrganizationTools: CommonOrganizationTools
	{
		public DateTime CreatedDate { get; set; }
		public string? CreatedUser { get; set; }
	}
	public class UpdateOrganizationTools: CommonOrganizationTools
	{
		public int OrgToolID { get; set; }
		public DateTime ModifiedDate { get; set; }
		public string? ModifiedUser { get; set; }
	}
	public class OrganizationToolModel:UpdateOrganizationTools
	{
		public DateTime CreatedDate { get; set; }
		public string? CreatedUser { get; set; }
	}
}
