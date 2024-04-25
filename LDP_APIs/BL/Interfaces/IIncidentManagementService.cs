using LDP_APIs.Models;

namespace LDP_APIs.Interfaces
{
    public interface IIncidentManagementService
    {
        Task<string> CreateIncident(CreateIncidentDTO request);
        Task<GetIncidentsResponse> GetIncidents();
    }
}
