using System.ComponentModel.DataAnnotations;

namespace LDPRuleEngine.DAL.Entities
{
    public class PlayBook
    {
		[Key]
		public int Play_Book_ID { get; set; }
		public string? Alert_Catogory { get; set; }
		public string? Play_Book_Name { get; set; }
		public string? Remarks { get; set; }
		public int active { get; set; }
		public DateTime? Created_Date { get; set; }
		public DateTime? Modified_Date { get; set; }
		public string? Created_User { get; set; }
		public string? Modified_User { get; set; }
		public int Processed { get; set; }
	}
}
