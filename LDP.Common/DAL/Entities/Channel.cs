using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities
{
    public class LDCChannel
    {
        [Key]
        public int channel_id { get; set; }
        public string? channel_Name { get; set; }
        public string? channel_Description { get; set; }
        public int channel_type_id { get; set; }
        public string? channel_type_name { get; set; }
        public int active { get; set; }
        public int display_order { get; set; }
        public DateTime? Created_date { get; set; }

        public string? Created_user { get; set; }

        public DateTime? Modified_date { get; set; }
       
        public string? Modified_user { get; set; }
        public int org_id { get; set; }

        public DateTime? deleted_date { get; set; }

        public string? deleted_user { get; set; }

        public string? msteams_teamsid { get; set; }

        public string? msteams_channelid { get; set; }

    }
}
