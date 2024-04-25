using System.ComponentModel.DataAnnotations;

namespace LDP_APIs.DAL.Entities
{
    public class LDPTool
    {
		[Key]
		public int Tool_ID { get; set; }
		public string? Tool_Name { get; set; }

        public string? Tool_Type { get; set; }

        public DateTime? Created_date { get; set; }
        public DateTime? Modified_date { get; set; }
        public string? Created_user { get; set; }
        public string? Modified_user { get; set; }
        public int Processed { get; set; }

        public int Tool_Type_ID { get; set; }

        public int active { get; set; }
        
        public DateTime? deleted_date { get; set; }
        public string? deleted_user { get; set; }
    }
}
