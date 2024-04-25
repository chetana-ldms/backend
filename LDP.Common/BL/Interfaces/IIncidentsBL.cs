using LDP.Common.DAL.Entities;
using LDP.Common.Requests;
using LDP.Common.Responses;
using LDP_APIs.BL.APIRequests;
using LDP_APIs.Models;

namespace LDP.Common.BL.Interfaces
{
    public interface IIncidentsBL
    {
        CreateIncidentResponse CreateIncident(CreateIncidentRequest request);
        GetIncidentsResponse GetIncidents(GetIncidentsRequest request);

        GetIncidentResponse GetIncidentDetails(int incidentId);

        getUnattendedIncidentcountResponse GetUnAttendedIncidentsCount(GetUnAttendedIncidentCount request);

        IncidentAssignOwnerResponse AssignOwner(IncidentAssignOwnerRequest request);

        SetIncidentStatusResponse SetIncidentStatus(SetIncidentStatusRequest request);
        SetIncidentPriorityResponse SetIncidentPriority(SetIncidentPriorityRequest request);
        SetIncidentSeviarityResponse SetIncidentSeviarity(SetIncidentSeviarityRequest request);

        SetIncidentScoreResponse SetIncidentScore(SetIncidentScoreRequest request);

        SetIncidentTypeResponse SetIncidentType(SetIncidentTypeRequest request);

        GetIncidentsResponse GetMyInternalIncidents(GetMyInternalIncidentsRequest request);

        GetIncidentCountByPriorityAndStatusResponse GetIncidentCountByPriorityAndStatus(GetIncidentCountByPriorityAndStatusRequest request);

        SearchIncidentsResponse GetIncidentSearchResult(IncidentSeeachRequest request);

        UpdateIncidentResponse UpdateIncident(UpdateIncidentRequest request);

    }
}
