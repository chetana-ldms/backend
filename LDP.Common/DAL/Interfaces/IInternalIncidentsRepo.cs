using LDP.Common.DAL.Entities;
using LDP.Common.Requests;
using LDP.Common.Responses;
//using SetIncidentScoreRequest = LDP_APIs.BL.APIRequests.SetIncidentScoreRequest;

namespace LDP.Common.DAL.Interfaces
{
    public interface IInternalIncidentsRepository
    {
       Task<CreateIncidentInternalResponse> CreateInternalIncident(Incident request);
       Task<List<Incident>> GetInternalIncidents(GetIncidentsRequest request, bool adminUser);
       Task<string> UpdateIncident(Incident request);
       Task<Incident> GetInternalIncidentData(GetInternalIncidentDataRequest request);
       Task<double> GetInternalIncidentsCount(GetIncidentsRequest request, bool adminUser);

       Task<int> GetUnAttendedIncidentsCount(GetUnAttendedIncidentCount request, int masterdataID);
       Task<string> AssignOwner(IncidentAssignOwnerRequest request);
       Task<string> SetIncidentStatus(SetIncidentStatusRequest request);
       Task<string> SetIncidentPriority(SetIncidentPriorityRequest request);
       Task<string> SetIncidentSeviarity(SetIncidentSeviarityRequest request);
       Task<string> SetIncidentType(SetIncidentTypeRequest request);
       Task<string> SetIncidentScore(SetIncidentScoreRequest request);
       Task<List<Incident>> GetMyInternalIncidents(GetMyInternalIncidentsRequest request);
       Task<double> GetIncidentCountByPriorityAndStatus(GetIncidentCountByPriorityAndStatusRequest request);

       Task<List<Incident>> GetIncidentSearchResult(IncidentSeeachRequest request, bool adminUser, string sortOption);

       Task<double> GetIncidentSearchResultCount(IncidentSeeachRequest request, bool adminUser);


    }
}
