using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities
{
    public class ChannelUploadFile
    {
        [Key]
        public int file_id { get; set; }
        public int org_id { get; set; }
        public int channel_id { get; set; }
        public int subitem_id { get; set; }
        public string? file_name { get; set; }
        public string? file_url { get; set; }

        public string? file_physical_path { get; set; }

        public int active { get; set; }
        public DateTime? created_date { get; set; }
       // public DateTime? modified_date { get; set; }
        public string? created_user { get; set; }
        //public string? modified_user { get; set; }
    }
}
