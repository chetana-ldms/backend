using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities
{
    public class ChannelSubItem
    {
        [Key]
        public int channel_subitem_id { get; set; }
        public int channel_id { get; set; }
        public string? channel_subitem_Name { get; set; }
        public string? api_url { get; set; }

        public string? document_url { get; set; }

        public string? channel_subitem_Description { get; set; }

        public int active { get; set; }
        public DateTime? Created_date { get; set; }
        public DateTime? Modified_date { get; set; }
        public string? Created_user { get; set; }
        public string? Modified_user { get; set; }

        public DateTime? deleted_date { get; set; }

        public string? deleted_user { get; set; }

    }
}
