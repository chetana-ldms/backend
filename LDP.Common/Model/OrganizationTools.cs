using LDP.Common.Model;

namespace LDP_APIs.BL.Models
{
    public class CommonOrganizationTools
    {
		public int ToolID { get; set; }
		public int OrgID { get; set; }
		public string? AuthKey { get; set; }
		//public double LastReadPKID { get; set; }
		//public string? ApiUrl { get; set; }

        //public string? ApiVerson { get; set; }
       // public int GetDataBatchSize { get; set; }
		

    }
	public class AddOrganizationTools: CommonOrganizationTools
	{
		public DateTime CreatedDate { get; set; }
        //public string? CreatedUser { get; set; }

        public int CreatedUserId { get; set; }
       // public string? LastReadAlertDate { get; set; }

		public List<AddOrganizationToolActionModel>? ToolActions { get; set; }
    }
    public class UpdateOrganizationTools: CommonOrganizationTools
	{
		public int OrgToolID { get; set; }
		public DateTime? ModifiedDate { get; set; }
        //public string? ModifiedUser { get; set; }  ModifiedUserId

        public int ModifiedUserId { get; set; }
        // public string? LastReadAlertDate { get; set; }

        public List<UpdateOrganizationToolActionModel>? ToolActions { get; set; }

    }
	public class OrganizationToolModel:UpdateOrganizationTools
	{
		public DateTime? CreatedDate { get; set; }
		public string? CreatedUser { get; set; }
		public DateTime? DeletedDate { get; set; }
		public string? DeletedUser { get; set; }

		public string? OrgName { get; set; }

		public string? ToolName { get; set; }

		public string? ModifiedUser { get; set;}

        public int ToolTypeId { get; set; }

        public string? ToolTypeName { get; set; }

        public List<OrganizationToolActionModel>? ToolActions { get; set; }

		public double LastReadPKID { get; set; }
		public string? ApiUrl { get; set; }

		public string? ApiVerson { get; set; }
		public int GetDataBatchSize { get; set; }
	}

    public class DeleteOrganizationTools 
	{
		public int OrgToolID { get; set; }
		public DateTime? DeletedDate { get; set; }
		public int DeletedUserId { get; set; }
	}
}
