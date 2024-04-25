using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities
{
    public class Tasks
    {
        [Key]
        public int task_id { get; set; }
        public string? task_type { get; set; }
        public string? task_title { get; set; }
        public int owner_Id { get; set; }
        public string? status { get; set; }
        public string? task_description { get; set; }
        public int task_for_user_id { get; set; }
        public int org_id { get; set; }
        public DateTime? started_date { get; set; }
        public DateTime? closed_date { get; set; }
        public string? created_user { get; set; }
        public DateTime? created_date { get; set; }
        public string? modified_user { get; set; }
        public DateTime? modified_date { get; set; }

        public string? cancelled_user { get; set; }
        public DateTime? cancelled_date { get; set; }

    }
}
