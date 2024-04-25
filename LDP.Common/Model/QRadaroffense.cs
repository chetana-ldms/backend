namespace LDP_APIs.Models
{
    public class QRadaroffense
    {
        public string? description { get; set; }
        public double id { get; set; }
        public double offense_type { get; set; }
        public string? offense_source { get; set; }

        public string? status { get; set; }

        public bool inactive { get; set; }

        public List<string>? categories { get; set; }

        public double last_persisted_time { get; set; }

        public double username_count { get; set; }

        public List<offenseRule>? rules { get; set; }

        public double event_count { get; set; }

        public double flow_count { get; set; }

        public string? assigned_to { get; set; }

        public double security_category_count { get; set; }

        public bool follow_up { get; set; }

        public List<double> source_address_ids { get; set; }

        public int source_count { get; set; }

        // public bool protected { get; set; }

        public string? source_network { get; set; }

        public List<string> destination_networks { get; set; }

        public double remote_destination_count { get; set; }

        public double start_time { get; set; }

        public double credibility { get; set; }

        public double magnitude { get; set; }

        public double severity { get; set; }

        public List<logSource>? log_sources { get; set; }

        public double policy_category_count { get; set; }

        public double device_count { get; set; }

        // public double first_persisted_time { get; set; }

        // public int MyProperty { get; set; }

        public double relevance { get; set; }

        public double domain_id { get; set; }

        public List<double>? local_destination_address_ids { get; set; }

        public int local_destination_count { get; set; }

    }

    public class offenseRule
    {
        public int id { get; set; }
        public string?  type { get; set; }
    }

    public class logSource
    {
        public int type_id { get; set; }

        public string? name { get; set; }

        public double  id { get; set; }

        public string?  type_name { get; set; }
    }
}
