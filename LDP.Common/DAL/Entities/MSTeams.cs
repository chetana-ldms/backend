using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities
{
    public class MSTeam
    {
        [Key]
        public int team_id { get; set; }
        public int org_id { get; set; }
        public string? team_name { get; set; }
        public string? ms_team_id { get; set; }
        public int default_team { get; set; }
        public int active { get; set; }
        public DateTime? created_date { get; set; }
        public DateTime? modified_date { get; set; }
        public string? created_user { get; set; }
        public string? modified_user { get; set; }
    }
}
