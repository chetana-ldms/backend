using LDP_APIs.BL.Models;

namespace LDP_APIs.Models
{
    public class CreateIncidentRequest: baseRequest
    {
        //public List<Incidentdtls>? IncidentdtlsList { get; set; }

        public List<AlertModel> AlertData { get; set; }
    }
}
