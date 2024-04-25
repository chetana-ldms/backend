using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities.Common
{
    public class LdcActivity
    {
        [Key]
        public int? activity_id { get; set; }
        public int? activity_type_id { get; set; }
        public int? org_id { get; set; }
        public int? alert_id { get; set; }
        public int? incident_id { get; set; }
        public string? primary_description { get; set; }
        public string? secondary_description { get; set; }
        public string? source { get; set; }
        public int? activity_exist_tool_and_ldc { get; set; }
        public int? tool_id { get; set; }
        public DateTime? activity_date { get; set; }
        public int? created_user_id { get; set; }
        public string? created_user { get; set; }
        public DateTime? created_date { get; set; }
       
    }

}
