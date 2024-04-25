using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.BL.Models;
using LDP_APIs.Models;

namespace LDP_APIs.Interfaces
{
    public interface IIncidentManagementService
    {
        Task<CreateIncidentInternalResponse> CreateIncident(CreateIncidentDTO request);
        Task<GetIncidentsResponse> GetIncidents(GetIncidentsRequest request, OrganizationToolModel connectiondtl);
        Task<GetIncidentsResponse> GetIncidentsByClientToolPKIds(List<string> ClientToolPKIds, OrganizationToolModel connectiondtl);
    }
}
 