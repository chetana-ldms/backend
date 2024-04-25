using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities.Common
{
    public class LDCApiUrls
    {
        [Key]
        public int url_id { get; set; }
        public int org_id { get; set; }
        public string? url_group { get; set; }
        public string? action_name { get; set; }
        public string? action_url { get; set; }
        public string? auth_token { get; set; }
        public int active { get; set; }
        public DateTime? created_Date { get; set; }
        public DateTime? modified_Date { get; set; }
        public string? created_User { get; set; }
        public string? modified_User { get; set; }
    }
}
