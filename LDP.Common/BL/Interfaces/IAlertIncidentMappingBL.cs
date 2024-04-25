using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.BL.Interfaces
{
    public interface IAlertIncidentMappingBL
    {
        GetAlertIncidentMappingResponse GetAlertIncidentMapping(GetAlertIncidentMappingRequest request);
        AlertIncidentMappingResponse AddAlertIncidentMapping(AddAlertIncidentMappingRequest request);
        AlertIncidentMappingResponse AddRangeAlertIncidentMapping(List<AddAlertIncidentMappingRequest> request);
        AlertIncidentMappingResponse UpdateAlertIncidentMapping(UpdateAlertIncidentMappingRequest request);

        GetAlertIncidentMappingResponse GetAlertIncidentMappingByIncidentIDs(List<double> incidentIds);

        List<GetAlertsByIncidentIdModel> GetAlertsByIncidentId(GetAlertsByIncidentIdRequest request);

        GetAlertIncidentMappingSingleResponse GetAlertIncidentMappingByIncidentID(int incidentId);

        AlertIncidentMappingResponse UpdateSignificantFlag(int incidentId , int significantFlag);

    }
}
