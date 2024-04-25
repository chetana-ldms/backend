using System.ComponentModel.DataAnnotations;

namespace LDP_APIs.DAL.Entities
{
    public class OganizationTool
    {
		[Key]
		public int Org_Tool_ID { get; set; }
		public int Tool_ID { get; set; }
		public int Org_ID { get; set; }
		public string? Auth_Key { get; set; }
		public double Last_Read_PKID { get; set; }
		public string? Api_Url { get; set; }
		//public int Active { get; set; }
		public DateTime? Created_Date { get; set; }
		public DateTime? Modified_Date { get; set; }
		public string? Created_User { get; set; }
		public string? Modified_User { get; set; }
		//public int Processed { get; set; }

	}
}
