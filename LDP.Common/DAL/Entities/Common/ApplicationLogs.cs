using System.ComponentModel.DataAnnotations;

namespace LDP.Common.DAL.Entities
{

    public class ApplicationLog
    {
        [Key]
        public int log_id { get; set; }
        public DateTime? log_date { get; set; }
        public string? severity { get; set; }
        public int? user_id { get; set; }
        public string? ip_address { get; set; }
        public string? log_source { get; set; }
        public string? message { get; set; }
        public string? stack_trace { get; set; }
        public string? request_data { get; set; }
        public string? response_data { get; set; }
        public string? additional_info { get; set; }
        public int? org_id { get; set; }
    }


}



