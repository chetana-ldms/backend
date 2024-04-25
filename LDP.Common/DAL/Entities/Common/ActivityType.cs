using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities.Common
{
    public class ActivityType
    {
        [Key]
        public int? activity_type_id { get; set; }
        public string? type_name { get; set; }
        public string? template { get; set; }
        public string? createed_user { get; set; }
        public DateTime? Created_date { get; set; }
    }
}
