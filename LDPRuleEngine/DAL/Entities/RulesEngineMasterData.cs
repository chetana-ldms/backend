using System.ComponentModel.DataAnnotations;

namespace LDPRuleEngine.DAL.Entities
{
    public class RulesEngineMasterData
    {
		[Key]
		public int master_data_id { get; set; }
		public string? master_data_type { get; set; }
		public string? master_data_name { get; set; }
		public int active { get; set; }
		public string? master_data_value { get; set; }
		public string? Map_Ref_Identifier { get; set; }
		public DateTime? Created_Date { get; set; }
		public DateTime? Modified_Date { get; set; }
		public string? Created_User { get; set; }
		public string? Modified_User { get; set; }
		public int Processed { get; set; }
	}
}
