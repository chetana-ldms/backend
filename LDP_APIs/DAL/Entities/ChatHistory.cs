using System.ComponentModel.DataAnnotations;

namespace LDP_APIs.DAL.Entities
{
    public class ChatHistory
    {
        [Key]
        public int chat_id { get; set; }

        public int org_id { get; set; }

        public string? from_user_name { get; set; }

        public int from_user_id { get; set; }
        public int to_user_id { get; set; }

        public string? to_user_name { get; set; }
        public string? chat_message { get; set; }
                
        public string? message_type { get; set; }
        public string? chat_subject { get; set; }
        public int subject_refid { get; set; }

        public string? attachment_url { get; set; }

        public string? attachment_physical_path { get; set; }

        public DateTime? created_date { get; set; }
        public string? created_user { get; set; }
        public DateTime? modified_date { get; set; }
        public string? modified_user { get; set; }


    }
}
