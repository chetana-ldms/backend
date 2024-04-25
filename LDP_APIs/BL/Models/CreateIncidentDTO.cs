using LDP_APIs.BL.Models;

namespace LDP_APIs.Models
{
    public class CreateIncidentDTO
    {
        //public Incidentdtls IncidentData { get; set; }
        //public IncidentsSystemAuthDtls AuthData { get; set; }
        public string? AuthKey { get; set; }

        public string? APIUrl { get; set; }
        public int ToolID { get; set; }

        public int OrgID { get; set; }

        public AlertModel? AlertData { get; set; }

    }
}
