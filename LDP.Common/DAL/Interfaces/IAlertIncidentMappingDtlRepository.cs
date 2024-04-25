using LDP.Common.DAL.Entities;
using LDP.Common.Model;
using LDP.Common.Requests;
using LDP.Common.Responses;

namespace LDP.Common.DAL.Interfaces
{
    public interface IAlertIncidentMappingRepository
    {
        Task<string> AddAlertIncidentMappingDtl(AlertIncidentMappingDtl request);
        Task<string> AddRangeAlertIncidentMappingDtl(List<AlertIncidentMappingDtl> request);

        Task<AlertIncidentMappingResponse> AddAlertIncidentMapping(AddAlertIncidentMappingRequest request);
        Task<string> AddRangeAlertIncidentMapping(List<AddAlertIncidentMappingRequest> request);

        Task<List<AlertIncidentMapping>> GetAlertIncidentMappingByIncidentIDs(List<double> incidentIds);


        Task<List<AlertIncidentMappingDtl>> GetAlertIncidentMappingDtlByMappingIDs(List<int> mappingIDs);
        Task<List<GetAlertsByIncidentIdModel>> GetAlertsByIncidentId(GetAlertsByIncidentIdRequest request);

        Task<AlertIncidentMapping> GetAlertIncidentMappingByIncidentID(double incidentId);

        Task<List<AlertIncidentMappingDtl>> GetAlertIncidentMappingDtlByMappingID(double mappingID);

        Task<string> UpdateSignificantFlag(int incidentId, int significantFlag);


    }
}
