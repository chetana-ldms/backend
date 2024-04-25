using LDP_APIs.Models;

namespace LDP_APIs.BL.ToolActions
{
    public class TicketManagementToolActionRequest: ToolActionRequest
    {
        public List<Incidentdtls> IncidentdtlsList { get; set; }
    }
}
