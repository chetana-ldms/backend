using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities
{
    public class alert_note
    {
        [Key]
        public int alerts_notes_id { get; set; }
        public double alert_id { get; set; }
        public string? action_type { get; set; }
        public string? action_name { get; set; }
        public double action_id { get; set; }
        public string? notes { get; set; }
        public DateTime? notes_date { get; set; }
        public int notes_created_userid { get; set; }
        public int notes_to_userid { get; set; }
        public DateTime? Created_date { get; set; }
        public string? Created_user { get; set; }

    }
}
