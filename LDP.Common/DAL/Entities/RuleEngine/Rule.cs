using System.ComponentModel.DataAnnotations;

namespace LDPRuleEngine.DAL.Entities
{
    public class LDPRule
    {
		[Key]
		public int Rule_ID { get; set; }
		public string? Rule_Name { get; set; }
		public int Rule_Catagory_ID { get; set; }
		//public string? Rule_Run_Attribute_Name { get; set; }
        public int active { get; set; }
		public DateTime? Created_Date { get; set; }
		public DateTime? Modified_Date { get; set; }
		public string? Created_User { get; set; }
		public string? Modified_User { get; set; }
		public int Processed { get; set; }

        public int org_id { get; set; }
        public DateTime? deleted_date { get; set; }
		public string? deleted_user { get; set; }
	}
}
