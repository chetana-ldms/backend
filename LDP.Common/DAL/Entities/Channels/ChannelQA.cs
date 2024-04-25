using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities
{
    public class ChannelQA
    {
        [Key]
        public int channel_qa_id { get; set; }
        public string? qa_type { get; set; }
        public string? qa_description { get; set; }
        public int qa_parent_refid { get; set; }
        public int channel_id { get; set; }
        public int org_id { get; set; }
        public int active { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public string? created_user { get; set; }
        public string? modified_user { get; set; }

        public DateTime? deleted_date { get; set; }

        public string? deleted_user { get; set; }
    }
}
