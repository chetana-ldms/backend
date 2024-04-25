namespace LDP_APIs.Models
{
    public class FreshDeskIncidentDtls
    {
        //public int id { get; set; }
        public int status { get; set; }
        public int priority { get; set; }
        public string? email { get; set; }

       // public string? Contact { get; set; }

        public string?  subject { get; set; }

        public string? description { get; set; }

        //public string? type { get; set; }

       // public string? phone { get; set; }

        //public string? name { get; set; }

        //public int Source { get; set; }
                

        // public string? unique_external_id { get; set; }

        // public string? Agents { get; set; }

        // public string? Groups { get; set; }

        // public DateTime? Created { get; set; }

        // public string? Resolution_due_by{ get; set; }

        //public string? First_response_due_by{ get; set; }

        //public string? Next_response_due_by{ get; set; }

        //public string? Skill { get; set; }

        // public DateTime? created_at { get; set; }

        //public DateTime? updated_at { get; set; }

        // public double form_id { get; set; }

        //public double requester_id { get; set; }

    }

    public class FreshDeskIncidentResponse
    {//
        public List<string>? cc_emails { get; set; }
        public List<string>? fwd_emails { get; set; }
        public List<string>? reply_cc_emails { get; set; }
        public List<string>? ticket_cc_emails { get; set; }
        public bool fr_escalated { get; set; }
        public bool spam { get; set; }
        public string? email_config_id { get; set; }
        public string? group_id { get; set; }
        public int priority { get; set; }
        public long requester_id { get; set; }
        public string? responder_id { get; set; }
        public int source { get; set; }
        public string? company_id { get; set; }
        public int status { get; set; }
        public string? subject { get; set; }
        public string? support_email { get; set; }
        public string? to_emails { get; set; }
        public string? product_id { get; set; }
        public double id { get; set; }
        public string? type { get; set; }
        public DateTime? due_by { get; set; }
        public DateTime? fr_due_by { get; set; }
        public bool is_escalated { get; set; }
        public string? description { get; set; }
        public string? description_text { get; set; }
       // public string? custom_fields { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public List<string>? tags { get; set; }
       // public List<string>? attachments { get; set; }
        public long form_id  { get; set; }
        //public string? nr_due_by { get; set; }
        //public string? nr_escalated { get; set; }

    }
}
