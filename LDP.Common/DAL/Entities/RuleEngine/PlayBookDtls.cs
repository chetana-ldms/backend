using System.ComponentModel.DataAnnotations;

namespace LDPRuleEngine.DAL.Entities
{
    public class PlayBookDtl
    {
		[Key]
		public int Play_Book_dtls_ID { get; set; }
		public int Play_Book_ID { get; set; }
		public string? Play_Book_Item_Type { get; set; }
		public int Play_Book_Item_Type_RefID { get; set; }
		public int Execution_Sequence_Number { get; set; }
		public int active { get; set; }
		public DateTime? Created_Date { get; set; }
		public DateTime? Modified_Date { get; set; }
		public string? Created_User { get; set; }
		public string? Modified_User { get; set; }
		public int Processed { get; set; }
	}
}
