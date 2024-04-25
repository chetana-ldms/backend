using LDP_APIs.Models;

namespace LDP_APIs.Interfaces
{
    public interface IIncidentManagementBL
    {
        string CreateIncident(CreateIncidentRequest requestlist);
        Task<GetIncidentsResponse> GetIncidents();

    }
}
