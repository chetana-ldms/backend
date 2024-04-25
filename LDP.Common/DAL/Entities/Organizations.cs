using System.ComponentModel.DataAnnotations;

namespace LDP_APIs.DAL.Entities
{
    public class Organization
    {
		[Key]
		public int Org_ID { get; set; }
		public string? Org_Name { get; set; }
		public string? Address { get; set; }
		public string? MobileNo { get; set; }
		public string? Email { get; set; }
		public DateTime? Created_Date { get; set;}
		public DateTime? Modified_Date { get; set;}
		public string? Created_User { get; set;}
		public string? Modified_User { get; set;}
		public int Processed { get; set; }

		public int active { get; set; }
		public DateTime? deleted_date { get; set; }
		public string? deleted_user { get; set; }

        public int manage_internal_incidents { get; set; }
        
    }
}
